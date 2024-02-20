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
    public GameObject clonnedThrowHook = null;
    Rigidbody rb;
    public bool isCollidingWithClone = false;
    void Awake()
    {
        cam = Camera.main;
    }
    
    public void DestoryGGH() {
     //Destroy(clonedGrapple);
        Destroy(clonnedThrowHook);
        
    }
   

    public void FireGrappleHook(GameObject thk)
    {
        if (!isFired)
        { print("Firing");
            cam = Camera.main;

            clonnedThrowHook = thk;
            //Debug.Log(clonnedThrowHook);

            rb = clonnedThrowHook.GetComponent<Rigidbody>();
            rb.AddForce(cam.transform.forward * launchSpeed + UnityEngine.Vector3.up * upwardForce, ForceMode.Impulse);
            
            isFired = true;
            isPulled = false;

        }
        else if(isFired && !isPulled)
        {
            //fuck with this later get it to only do this when it stops
            Debug.Log("Pulling");
            isPulled = true;
            rb.isKinematic = true;
            
        } else if (isFired /*&& isPulled*/)
            {
                print("Destroying");
                isFired = false;
                isPulled= false;
                isCollidingWithClone = false;
                DestoryGGH();
                clonnedThrowHook = null;
            }
        }
                   
    }

