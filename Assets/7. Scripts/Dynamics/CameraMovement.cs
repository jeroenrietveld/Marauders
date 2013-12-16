using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour
{
	private List<GameObject> _trackableObjects = new List<GameObject>();
	private List<Vector3> _vectorBuffer = new List<Vector3>();
	private List<GameObject> _occluders = new List<GameObject>();
	public float maxPlayerAngle = 30f;
	public const float degToRad = 1 / (360 / Mathf.PI);
	
	public float cameraSmoothing = 5f;
	public float minCameraDistance = 10f;
	public float maxCameraDistance = 100f;
	
	public int solverIterations = 5;

	void FixedUpdate()
	{
		UpdateCameraPosition();
		//UpdateOccluderTransparency();
	}
	
	void UpdateCameraPosition()
	{
		transform.position = Vector3.zero;
		for(int _ = 0; _ < solverIterations; ++_)
		{
			UpdatePositionIteration();
		}
		//transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraSmoothing);
	}
	
	void UpdateOccluderTransparency()
	{
		foreach (GameObject obj in _occluders)
		{
			if(obj != null)
			{
				Color color = obj.renderer.material.color;
				color.a = 1.0f;
				
				obj.renderer.material.color = color;
			}
		}
		
		_occluders.Clear();
		
		foreach (GameObject obj in _trackableObjects)
		{
			Vector3 rayDirection = obj.transform.position + new Vector3(0, 0.1f, 0) - transform.position;
			RaycastHit[] hits = Physics.RaycastAll(transform.position, rayDirection.normalized, rayDirection.magnitude);
			
			foreach (RaycastHit hit in hits)
			{
				GameObject hitObj = hit.collider.gameObject;
				if (hitObj != obj && !_occluders.Contains(hitObj) && !_trackableObjects.Contains(hitObj) && hitObj.renderer != null)
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


	private Vector3 GetCenter(List<Vector3> positions)
	{
		Vector3 min, max;
		min = max = Vector3.zero;

		GetMinMax(positions, ref min, ref max);
		return (min + max) / 2;
	}

	private void GetMinMax(List<Vector3> positions, ref Vector3 min, ref Vector3 max)
	{
		if(positions.Count == 0) return;

		min = positions[0];
		max = positions[0];

		for (int i = 1; i < _trackableObjects.Count; i++)
		{
			min.x = Mathf.Min(min.x, positions[i].x);
			min.y = Mathf.Min(min.y, positions[i].y);
			min.z = Mathf.Min(min.z, positions[i].z);
			
			max.x = Mathf.Max(max.x, positions[i].x);
			max.y = Mathf.Max(max.y, positions[i].y);
			max.z = Mathf.Max(max.z, positions[i].z);
		}
	}

	private void UpdatePositionIteration()
	{
		_vectorBuffer.Clear();
		foreach(var obj in _trackableObjects) _vectorBuffer.Add(camera.WorldToViewportPoint(obj.transform.position));
		Vector3 playerCenter = camera.ViewportToWorldPoint(GetCenter(_vectorBuffer));

		_vectorBuffer.Clear();
		foreach (GameObject player in _trackableObjects)
		{
			_vectorBuffer.Add(Project(playerCenter, -transform.forward, player.transform.position));
		}
		
		float distanceScale = 1 / Mathf.Tan(maxPlayerAngle * degToRad);
		
		float maxDistance = float.NegativeInfinity;
		
		for (int i = 0; i < _trackableObjects.Count; i++)
		{
			float distance = Vector3.Distance(_trackableObjects[i].transform.position, _vectorBuffer[i]);
			
			float cameraDistance = Vector3.Distance(
				_vectorBuffer[i] - (distance * distanceScale) * transform.forward,
				playerCenter);
			
			maxDistance = Mathf.Max(maxDistance, cameraDistance);
		}
		
		maxDistance = Mathf.Clamp(maxDistance, minCameraDistance, maxCameraDistance);
		
		Vector3 targetPosition = playerCenter - transform.forward * maxDistance;
		transform.position = targetPosition;
	}

}