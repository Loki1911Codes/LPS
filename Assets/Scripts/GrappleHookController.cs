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
    public float launchSpeed = 15f;
    public float upwardForce = 5f;
    public GameObject clonnedThrowHook;
    Rigidbody rb;
    public bool isCollidingWithClone = true;
    public void OnTriggerEnter(Collider other)
    {   
        if (gameObject.layer == LayerMask.NameToLayer("ThrowedHook")){
            isCollidingWithClone = false;
            Debug.Log("Passing");
            Debug.Log(gameObject.layer);
            if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                isCollidingWithClone = true;
                Debug.Log("isCollidingWithClone = " + isCollidingWithClone);
            }
        }else{
            isCollidingWithClone = true;
        }
    }
    
    public void DestoryGGH() {
     //Destroy(clonedGrapple);
        Destroy(clonnedThrowHook);
        
    }
    
   

    public void FireGrappleHook()
    {
     
        
        if (!isFired)
        { print("Firing");

            GameObject throwingHook = Instantiate(ThrownGHookPrefab, cam.transform.position + cam.transform.forward * 1f, Quaternion.identity);
            clonnedThrowHook = throwingHook;
            throwingHook.tag = "ThrowingHook"; 
            rb = clonnedThrowHook.GetComponent<Rigidbody>();
            //Debug.Log("Attemopting to fire with force = " + TrajVis.launchSpeed + " and angle = " + TrajVis.CalculateLaunchAngle());
            rb.AddForce(cam.transform.forward * launchSpeed + UnityEngine.Vector3.up * upwardForce, ForceMode.Impulse);
            isFired = true;
            isPulled = false;

        }
        else if(isFired && !isPulled)
        {   print("Attemping pull");
            //Debug.Log(isFired + " "  +isPulled);
            if (isCollidingWithClone)
            {
                Debug.Log("Pulling");
                isPulled = true;
                rb.isKinematic = true;
            }
        } else {
            if (isFired && isPulled)
            {
                print("Destroying");
                isFired = false;
                isPulled= false;
                isCollidingWithClone = false;
                DestoryGGH();
            }
        }
                   
    }
}
