using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GrappleHookController : MonoBehaviour
{
    public GameObject baseGHookPrefab;
    public LayerMask GrappleHook;
    public Camera cam;
    public float grappleHookScale = 0.036567f;
    private bool isFired = false;
    public GameObject clonedGrapple;
    public TrajectoryVisualiser TrajVis;
    public void DestoryGGH() {
     Destroy(clonedGrapple);
    }

    public void SpawnGrappleAtPoint(Vector3 hitpoint)
    {
        if (!isFired)
        {
            //Debug.Log("Attempting Spawn at = " + hitpoint);
            GameObject grappleHook = Instantiate(baseGHookPrefab, hitpoint, Quaternion.identity);
            grappleHook.layer = GrappleHook;
            grappleHook.transform.localScale = new Vector3(grappleHookScale, grappleHookScale, grappleHookScale);
            clonedGrapple = grappleHook;
            grappleHook.transform.LookAt(cam.transform.position);
            isFired = true;
            

        }else {
            //Code here to destroy the grapple hook, there is already one prefag called BaseGHook that is in the game and cant be destryoed, only the new just spawend one.
            isFired = false;
            DestoryGGH();
        }
                   
    }
    /*public void FiringAlongTraj()
{
    float launchAngle = TrajVis.CalculateLaunchAngle(); 
    float launchSpeed = TrajVis.launchSpeed;

    float time = 0f;
    float timeInterval = 0.1f; // Set the time interval for each iteration

    Vector3 launchPoint = transform.position;
    Vector3 launchDirection = transform.forward; 

    while (true)
    {
        float x = launchSpeed * Mathf.Cos(launchAngle * Mathf.PI / 180) * time;
        float y = TrajVis.initialHeight - 4.9f * Mathf.Pow((x / (launchSpeed * Mathf.Cos(launchAngle * Mathf.PI / 180))), 2) + Mathf.Tan(launchAngle * Mathf.PI / 180) * x;

        Vector3 nextPoint = launchPoint + launchDirection * time + new Vector3(x, y, launchSpeed * time);

        if (Physics.Raycast(nextPoint, launchDirection, out RaycastHit hit))
        {
            
            SpawnGrappleAtPoint(hit.point);
            break;
        }

        // Move the grapple game object along the trajectory
        transform.position = nextPoint;

        time += timeInterval;
    }
}*/
}
