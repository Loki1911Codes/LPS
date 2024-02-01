using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeChecker : MonoBehaviour
{
    public float raycastDis = 1.0f; // Length of the raycast
    public float angleAllowance = 15.0f; // Allowance for facing up angle
    /*void OnDrawGizmosSelected()
    {
        // Draw the raycast in the Scene view when the object is selected
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, -transform.up * raycastDis);
    }*/
   public bool Vault()
    {
        
        // Perform raycast from the object's position pointing down
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, raycastDis))
        {
            //Debug.DrawRay(transform.position, -transform.up * raycastDis, Color.green);
            // Get the normal of the surface the raycast hits
            Vector3 hitNormal = hit.normal;
            // Check if the hit surface is facing up within the specified allowance
            if (Vector3.Angle(hitNormal, transform.up) <= angleAllowance)
            {
                // Handle attempting vault
                //Debug.Log("Attempting vault");
                return true;
            }else {return false;}
        }else {return false;}

    }
}
