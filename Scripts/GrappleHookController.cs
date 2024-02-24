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
    public PlayerMotor pmotor;
    public bool isFired = false;
    public bool isPulled = false;
    public float launchSpeed = 15f;
    public float upwardForce = 5f;
    public GameObject clonnedThrowHook = null;
    public Rigidbody rb;
    public Vector3 grapplePOS;
    private GameObject grappleToRemove;
  
    
    public void DestoryGGH() {
     //Destroy(clonedGrapple);
        Destroy(clonnedThrowHook);
    }
   public void Awake()
   {
    isFired = false;
    isPulled = false;
   }

    public void FireGrappleHook(GameObject thk)
    {

        //if (!isFired)
         print("Firing");
            print(thk);
            cam = Camera.main;
            grappleToRemove = thk;
            //Debug.Log(clonnedThrowHook);

            rb = thk.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(cam.transform.forward * launchSpeed + UnityEngine.Vector3.up * upwardForce, ForceMode.Impulse);
            //Debug.Log(rb);

            isFired = true;
            isPulled = false;
    }
    public void SendStopToConnect()
    {
        print("Stopped");
        rb.isKinematic = true;
        isPulled = true;
    }
    public void RemoveGrapple()
    {
        rb.isKinematic = false;
        Destroy(grappleToRemove);
        isFired = false;
        isPulled = false;
    }
}
                   
    

