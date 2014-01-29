using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XInputDotNetPure;
public class SpectatorCamera : MonoBehaviour
{

    public float turnSpeed = 4.0f;		// Speed of camera turning when mouse moves in along an axis
	public float panSpeed = 4.0f;		// Speed of the camera when being panned
	private float zoomSpeed = 0f;		// Current Speed of the camera going back and forth
	public float zoomMaxSpeed = 0.4f;	// Maximum speed at which the camera should be moving (this is the fixed speed when flying around)
	public float zoomFactor = 24; // Zoomfactor is the speed at which the camera slows up and down when zooming in.
	
	private Vector3 mouseOrigin;	// Position of cursor when mouse dragging starts
	private bool isPanning;		// Is the camera being panned?
	private bool isRotating;	// Is the camera being rotated?
	private bool isZoomingIn;		// Is the camera zooming?
    private bool isZoomingOut;

	void start()
	{

	}

    void Update()
    {   
        // Get the left mouse button
		if(Input.GetMouseButtonDown(1))
		{
            mouseOrigin = Input.mousePosition;
            isRotating = true;
		}
		
		// Get the right mouse button
		if(Input.GetMouseButtonDown(0))
		{
            mouseOrigin = Input.mousePosition;
            isPanning = true;
		}
		
		// Checks the W key for general forward movement in 3D space. (also checks for camera smooth)
		if(Input.GetKey(KeyCode.W) || zoomSpeed > 0)
		{
            ZoomIn();
		}
        
		// Checks the WSkey for general backwards movement in 3D space. (also checks camera smooth)
		if (Input.GetKey(KeyCode.S) || zoomSpeed < 0)
        {
            ZoomOut();
        }
		
		// Disable movements on button release
		if (!Input.GetMouseButton(1)) isRotating=false;
		if (!Input.GetMouseButton(0)) isPanning=false;
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) isZoomingIn = false; isZoomingOut = false;
		
		// Move the camera on it's XY plane
		if (isPanning)
		{
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);
            transform.Translate(move, Space.Self);
		}

        if(isRotating)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
            transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
            transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
        }
		
	}

    void ZoomIn()
    {
		// checks if smooth speedup or speeddown
		if(!Input.GetKey(KeyCode.W)){
			if(zoomSpeed > 0){
				zoomSpeed -= zoomMaxSpeed / zoomFactor;
			}
		}else{
			if(zoomSpeed < zoomMaxSpeed){
				zoomSpeed += zoomMaxSpeed / zoomFactor;
			}
		}
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

		Vector3 move = pos.y * zoomSpeed * transform.forward;
        transform.Translate(move, Space.World);
    }

    void ZoomOut()
    {
		// checks if smooth speedup or speeddown
		if(!Input.GetKeyUp(KeyCode.S)){
			if(zoomSpeed > 0){
				zoomSpeed -= zoomMaxSpeed / zoomFactor;
			}
		}else{
			if(zoomSpeed < zoomMaxSpeed){
				zoomSpeed += zoomMaxSpeed / zoomFactor;
			}
		}
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

		Vector3 move = pos.y * -zoomSpeed * transform.forward;
        transform.Translate(move, Space.World);
    }
	
}

