using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour
{
	private IList<GameObject> trackableObjects = new List<GameObject>();
	private IList<Vector3> projectedPositions = new List<Vector3>();
	private IList<GameObject> occluders = new List<GameObject>();
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
		foreach (GameObject obj in occluders)
		{
			Color color = obj.renderer.material.color;
			color.a = 1.0f;
			
			obj.renderer.material.color = color;
		}
		
		occluders.Clear();
		
		foreach (GameObject obj in trackableObjects)
		{
			Vector3 rayDirection = obj.transform.position - transform.position;
			RaycastHit[] hits = Physics.RaycastAll(transform.position, rayDirection.normalized, rayDirection.magnitude);
			
			foreach (RaycastHit hit in hits)
			{
				GameObject hitObj = hit.collider.gameObject;
				if (hitObj != obj && !occluders.Contains(hitObj) && !trackableObjects.Contains(hitObj))
				{
					occluders.Add(hitObj);
				}
			}
		}
		
		foreach (GameObject obj in occluders)
		{
			Color color = obj.renderer.material.color;
			color.a = 0.5f;
			
			obj.renderer.material.color = color;
		}
	}
	
	void UpdateCameraPosition()
	{
		Vector3 minPosition, maxPosition;
		
		minPosition = trackableObjects[0].transform.position;
		maxPosition = trackableObjects[0].transform.position;
		
		for (int i = 1; i < trackableObjects.Count; i++)
		{
			minPosition.x = Mathf.Min(minPosition.x, trackableObjects[i].transform.position.x);
			minPosition.y = Mathf.Min(minPosition.y, trackableObjects[i].transform.position.y);
			minPosition.z = Mathf.Min(minPosition.z, trackableObjects[i].transform.position.z);
			
			maxPosition.x = Mathf.Max(maxPosition.x, trackableObjects[i].transform.position.x);
			maxPosition.y = Mathf.Max(maxPosition.y, trackableObjects[i].transform.position.y);
			maxPosition.z = Mathf.Max(maxPosition.z, trackableObjects[i].transform.position.z);
		}
		
		Vector3 playerCenter = (minPosition + maxPosition) / 2;
		
		Vector3 viewDirection = this.transform.forward;
		
		projectedPositions.Clear();
		foreach (GameObject player in trackableObjects)
		{
			projectedPositions.Add(Project(playerCenter, -viewDirection, player.transform.position));
		}
		
		float distanceScale = 1 / Mathf.Tan(maxPlayerAngle * degToRad);
		
		float maxDistance = float.NegativeInfinity;
		
		for (int i = 0; i < trackableObjects.Count; i++)
		{
			float distance = Vector3.Distance(trackableObjects[i].transform.position, projectedPositions[i]);
			
			float cameraDistance = Vector3.Distance(
									projectedPositions[i] - (distance * distanceScale) * viewDirection,
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
		trackableObjects.Add(obj);
	}
	
	public void RemoveTrackableObject(GameObject obj)
	{
		trackableObjects.Remove(obj);
	}
}