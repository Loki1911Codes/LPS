using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    public GameObject ThrownGHookPrefab;
    public GameObject throwingHook;
    private bool isGrounded;
    public GrappleHookController gHC;

    private CharacterController controller;
    private UnityEngine.Vector3 playerVelocity;
    public Camera cam;
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public bool lerpCrouch = false;
    public float crouchTimer = 1f;
    public bool crouching = false;
    public bool sprinting = false;
    public bool doubleJump = false;
    private bool usedDubJump = false;
    private bool vaultReset = true;
    public GameObject player;
    public Rigidbody rb;
    
    private SpringJoint joint;
    

    


    void Update()
    {
        
        if (isGrounded && usedDubJump && doubleJump)
        {
            usedDubJump = false;
        }
        //Debug.Log("IsGrouned =" + isGrounded + "& used Double Jump = " + usedDubJump);
        isGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gHC.isFired = false;
        gHC.isPulled = false;
        rb = player.GetComponent<Rigidbody>();
    }
    //receive input from manger and put into controller
    public void ProcessMove(UnityEngine.Vector2 input)
    {
        UnityEngine.Vector3 MoveDirection = UnityEngine.Vector3.zero;
        MoveDirection.x = input.x;
        MoveDirection.z = input.y;
        controller.Move(transform.TransformDirection(MoveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }
    public void Jump()
    {
        if (isGrounded)
        {
            vaultReset = true;
        }
        if (isGrounded && !doubleJump)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            usedDubJump = false;
        }else if (isGrounded && usedDubJump == false && doubleJump)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            usedDubJump = false;
        }else if (!isGrounded && usedDubJump == false && doubleJump)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            usedDubJump = true;
        }else if (isGrounded && usedDubJump)
        {
            usedDubJump = false;
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }
    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
            speed = 8; 
        else
            speed = 5;
    }
    public void JVaultLedge(bool canVault)

    {
        if (!isGrounded && canVault == true && vaultReset)
        {
           
            playerVelocity.y = Mathf.Sqrt((jumpHeight * 0.6f) * -3.0f * gravity);
            vaultReset = false;
        }
    
    }
    public void FireGrapple()
    {print(gHC.isFired + " and " + gHC.isPulled);
        if (gHC.isFired == false)
        {print("Instantiating Grapple");
            throwingHook = Instantiate(ThrownGHookPrefab, cam.transform.position + cam.transform.forward * 1f, UnityEngine.Quaternion.identity);
            gHC.FireGrappleHook(throwingHook);
        }else if (gHC.isPulled == false)
        {print("Stopping Grapple");
            gHC.SendStopToConnect();
            ConnectGrapple();
        }      
        else if (gHC.isFired && gHC.isPulled){
            print("Removing Grapple");
            gHC.RemoveGrapple();
            KillRb();
        }
    }
    //Make KillRb and Connect and Start disable and enable Collider respectivly then fig out joint
    public void KillRb()
    {
        rb.isKinematic = true;
    }
    public void ConnectGrapple()
    {
        rb.isKinematic = false;
    }
}
