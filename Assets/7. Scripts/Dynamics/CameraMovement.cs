using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour
{
	private List<GameObject> _trackableObjects = new List<GameObject>();
	private List<Vector3> _vectorBuffer = new List<Vector3>();
	private List<GameObject> _occluders = new List<GameObject>();
	public float maxPlayerAngle = 50f;
	public const float degToRad = 1 / (360 / Mathf.PI);

	public float yAxisTrackingCenter = 0;
	public float yAxisTrackingRange = 10;

	public float minCameraDistance = 10f;
	
	public int solverIterations = 5;

	void FixedUpdate()
	{
		UpdateCameraPosition();
		//UpdateOccluderTransparency();
	}
	
	void UpdateCameraPosition()
	{
		// Reset position instead of working from where we are to ensure inter frame stability.
		// Some distance from center of trackable objects in world space is a decent heuristic,
		// this means we need fewer iterations.
		_vectorBuffer.Clear();
		foreach(var obj in _trackableObjects) _vectorBuffer.Add(obj.transform.position);
		transform.position = GetCenter(_vectorBuffer) - transform.forward * 25;

		for(int _ = 0; _ < solverIterations; ++_)
		{
			UpdatePositionIteration();
		}
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

	// Project point 'v' on the line defined by point 'p' and direction 'd'
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

	// Uses the current transform to determine object position in screen space. Call multiple times to improve accuracy.
	private void UpdatePositionIteration()
	{
		_vectorBuffer.Clear();
		foreach(var obj in _trackableObjects) _vectorBuffer.Add(camera.WorldToViewportPoint(ClampPosition(obj)));
		Vector3 objectCenter = camera.ViewportToWorldPoint(GetCenter(_vectorBuffer));

		_vectorBuffer.Clear();
		foreach (GameObject obj in _trackableObjects)
		{
			_vectorBuffer.Add(Project(objectCenter, -transform.forward, ClampPosition(obj)));
		}
		
		float distanceScale = 1 / Mathf.Tan(maxPlayerAngle * degToRad);
		
		float maxDistance = float.NegativeInfinity;
		
		for (int i = 0; i < _trackableObjects.Count; i++)
		{
			float distance = Vector3.Distance(ScalePosition(ClampPosition(_trackableObjects[i])), _vectorBuffer[i]);
			
			float cameraDistance = Vector3.Distance(
				_vectorBuffer[i] - (distance * distanceScale) * transform.forward,
				objectCenter);
			
			maxDistance = Mathf.Max(maxDistance, cameraDistance);
		}
		
		maxDistance = Mathf.Max(maxDistance, minCameraDistance);
		
		Vector3 targetPosition = objectCenter - transform.forward * maxDistance;
		transform.position = targetPosition;
	}

	// Transform point to projection space, move origin to center of screen, 
	// apply aspect ratio correction, move origin back to ll corner,
	// transform result back to world space
	private Vector3 ScalePosition(Vector3 v)
	{
		return camera.ViewportToWorldPoint(
			Vector3.Scale(
				camera.WorldToViewportPoint(v) - new Vector3(.5f, .5f, 0),
				new Vector3(1 / camera.aspect, 1, 1)
			) + new Vector3(.5f, .5f, 0)
		);
	}

	private Vector3 ClampPosition(GameObject obj)
	{
		Vector3 pos = obj.transform.position;
		pos.y = Mathf.Clamp(pos.y, yAxisTrackingCenter - yAxisTrackingRange, yAxisTrackingCenter + yAxisTrackingRange);
		return pos;
	}

}