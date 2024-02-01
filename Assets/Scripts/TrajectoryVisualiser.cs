using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryVisualiser : MonoBehaviour
{
    
    public float launchSpeed;
    public float initialHeight;
    public Camera cam;
    public GrappleHookController gHC;


    public void SimulateTrajectory()
    {
        Debug.Log("TraVis Firing");
        Vector3 launchPoint = cam.transform.position;
        Vector3 launchDirection = cam.transform.forward;

        float timeInterval = 0.1f;
        float time = 0f;

        Vector3 previousPoint = launchPoint;

        while (true)
        {
            float launchAngle = CalculateLaunchAngle();
            float x = launchSpeed * Mathf.Cos(launchAngle * Mathf.PI / 180) * time;
            float y = (initialHeight - 4.9f * Mathf.Pow((x / (launchSpeed * Mathf.Cos(launchAngle * Mathf.PI / 180))), 2)) + Mathf.Tan(launchAngle * Mathf.PI / 180) * x;

            Vector3 nextPoint = launchPoint + launchDirection * time + new Vector3(x, y, launchSpeed * time);

            Debug.DrawLine(previousPoint, nextPoint, Color.red); // Draw a line segment from the previous point to the next point

            previousPoint = nextPoint;
            time += timeInterval;

            if (Physics.Raycast(previousPoint, launchDirection, out RaycastHit hit))
            {
                Debug.Log("Collided");
                gHC.SpawnGrappleAtPoint(hit.point);
                break;
            }
        }
    }

    float CalculateLaunchAngle()
    {
        if (cam != null)
        {
            // Calculate the direction the camera is facing
            Vector3 cameraDirection = cam.transform.forward;
            // Calculate the up and down angle of the camera
            float angle = Vector3.Angle(Vector3.up, cameraDirection);
            return angle + 90f;
        }
        else
        {
            Debug.LogError("Camera not assigned to the script");
            return 45f; 
        }
    }

}
