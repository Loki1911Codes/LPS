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
    private float GrappleCastDistance = 10f;
    public GameObject clonedGrapple;
    public void DestoryGGH() {
     Destroy(clonedGrapple);
    }
    /*public UnityEngine.Vector3 GrappleRayCheck()
    {
        RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, GrappleCastDistance))
            {
                UnityEngine.Vector3 hitPoint = hit.point;
                // Use the hitPoint for any further actions, such as spawning objects or effects
                Debug.Log("Hit point: " + hitPoint); // Log the hit point for debugging
                SpawnGrappleAtPoint(hitPoint);
        
                return UnityEngine.Vector3.zero;
            }else{  return UnityEngine.Vector3.zero; }
    
    }*/
    public void SpawnGrappleAtPoint(Vector3 hitpoint)
    {
        if (!isFired)
        {
            Debug.Log("Attempting Spawn at = " + hitpoint);
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
}
