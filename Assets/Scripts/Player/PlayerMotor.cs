using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMotor : MonoBehaviour
{
    private bool isGrounded;
    private CharacterController controller;
    private UnityEngine.Vector3 playerVelocity;
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public bool lerpCrouch = false;
    public float crouchTimer = 1f;
    public bool crouching = false;
    public bool sprinting = false;
    void Update()
    {
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
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
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
}

