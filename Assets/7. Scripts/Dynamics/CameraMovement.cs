using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour
{
	private IList<GameObject> _trackableObjects = new List<GameObject>();
	private IList<Vector3> _projectedPositions = new List<Vector3>();
	private IList<GameObject> _occluders = new List<GameObject>();
	public float maxPlayerAngle = 30f;
	public const float degToRad = 1 / (360 / Mathf.PI);
	
	public float cameraSmoothing = 5f;
	public float minCameraDistance = 10f;
	public float maxCameraDistance = 100f;
	
	void FixedUpdate()
	{
		UpdateCameraPosition();
		UpdateOccluderTransparency();
	}
	
	void UpdateOccluderTransparency()
	{
		foreach (GameObject obj in _occluders)
		{
			Color color = obj.renderer.material.color;
			color.a = 1.0f;
			
			obj.renderer.material.color = color;
		}
		
		_occluders.Clear();
		
		foreach (GameObject obj in _trackableObjects)
		{
			Vector3 rayDirection = obj.transform.position - transform.position;
			RaycastHit[] hits = Physics.RaycastAll(transform.position, rayDirection.normalized, rayDirection.magnitude);
			
			foreach (RaycastHit hit in hits)
			{
				GameObject hitObj = hit.collider.gameObject;
				if (hitObj != obj && !_occluders.Contains(hitObj) && !_trackableObjects.Contains(hitObj))
				{
					_occluders.Add(hitObj);
				}
			}
		}
		
		foreach (GameObject obj in _occluders)
		{
			Color color = obj.renderer.material.color;
			color.a = 0.5f;
			
			obj.renderer.material.color = color;
		}
	}
	
	void UpdateCameraPosition()
	{
		Vector3 minPosition, maxPosition;
		
		minPosition = _trackableObjects[0].transform.position;
		maxPosition = _trackableObjects[0].transform.position;
		
		for (int i = 1; i < _trackableObjects.Count; i++)
		{
			minPosition.x = Mathf.Min(minPosition.x, _trackableObjects[i].transform.position.x);
			minPosition.y = Mathf.Min(minPosition.y, _trackableObjects[i].transform.position.y);
			minPosition.z = Mathf.Min(minPosition.z, _trackableObjects[i].transform.position.z);
			
			maxPosition.x = Mathf.Max(maxPosition.x, _trackableObjects[i].transform.position.x);
			maxPosition.y = Mathf.Max(maxPosition.y, _trackableObjects[i].transform.position.y);
			maxPosition.z = Mathf.Max(maxPosition.z, _trackableObjects[i].transform.position.z);
		}
		
		Vector3 playerCenter = (minPosition + maxPosition) / 2;
		
		Vector3 viewDirection = this.transform.forward;
		
		_projectedPositions.Clear();
		foreach (GameObject player in _trackableObjects)
		{
			_projectedPositions.Add(Project(playerCenter, -viewDirection, player.transform.position));
		}
		
		float distanceScale = 1 / Mathf.Tan(maxPlayerAngle * degToRad);
		
		float maxDistance = float.NegativeInfinity;
		
		for (int i = 0; i < _trackableObjects.Count; i++)
		{
			float distance = Vector3.Distance(_trackableObjects[i].transform.position, _projectedPositions[i]);
			
			float cameraDistance = Vector3.Distance(
									_projectedPositions[i] - (distance * distanceScale) * viewDirection,
									playerCenter);
			
			maxDistance = Mathf.Max(maxDistance, cameraDistance);
		}
		
		maxDistance = Mathf.Clamp(maxDistance, minCameraDistance,maxCameraDistance);
		
		Vector3 targetPosition = playerCenter - viewDirection * maxDistance;
		
		this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * cameraSmoothing);
	}
	
	Vector3 Project(Vector3 p, Vector3 d, Vector3 v)
	{
		return Vector3.Dot(d, v - p) * d + p;
	}
	
	public void AddTrackableObject(GameObject obj)
	{
		_trackableObjects.Add(obj);
	}
	
	public void RemoveTrackableObject(GameObject obj)
	{
		_trackableObjects.Remove(obj);
	}
}