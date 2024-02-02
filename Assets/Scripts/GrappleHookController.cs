using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GrappleHookController : MonoBehaviour
{

    public GameObject ThrownGHookPrefab;
    public LayerMask GrappleHook;
    public Camera cam;
    private bool isFired = false;
    private bool isPulled = false;
    public TrajectoryVisualiser TrajVis;
    public GameObject clonnedThrowHook;
    public float upwardForce = 5f;
    Rigidbody rb;
    private bool isCollidingWithClone = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ThrowingHook")) // Replace "ThrowingGHook(Clone)" with the actual tag of the clone object
        {
            isCollidingWithClone = true;
        }else{isCollidingWithClone = false;}
    }
    
    void Update()
    {
        Debug.Log(isCollidingWithClone);
    }
    
    
    //public float upwardForce = 0.5f;
    
    public void DestoryGGH() {
     //Destroy(clonedGrapple);
        Destroy(clonnedThrowHook);
    }
    
   

    public void SpawnGrappleAtPoint()
    {
        
        if (!isFired)
        {

            GameObject throwingHook = Instantiate(ThrownGHookPrefab, cam.transform.position + cam.transform.forward * 1f, Quaternion.identity);
            clonnedThrowHook = throwingHook;
            throwingHook.tag = "ThrowingHook"; 
            rb = clonnedThrowHook.GetComponent<Rigidbody>();
            //Debug.Log("Attemopting to fire with force = " + TrajVis.launchSpeed + " and angle = " + TrajVis.CalculateLaunchAngle());
            rb.AddForce(cam.transform.forward * TrajVis.launchSpeed + UnityEngine.Vector3.up * upwardForce, ForceMode.Impulse);
            isFired = true;
            isPulled = false;

        }
        else if(isFired && !isPulled)
        {
            if (isCollidingWithClone){
                Debug.Log("stopping");
                isPulled = true;
            }
        } else {

            isFired = false;
            isPulled= false;
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
