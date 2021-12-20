using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController cc;
    [SerializeField] private Animator anim;

    [SerializeField] private float runSpeed = 5;
    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private float gravity = 30;
    [SerializeField] private float jumpSpeed = 8;
    [SerializeField] private float crouchSpeed = 1;
    private bool isJumping;
    private bool isCrouching;
    private float Horizontal { get { return Input.GetAxis("Horizontal"); } }
    private float Vertical { get { return Input.GetAxis("Vertical"); } }

    private Vector3 moveDirection;
    private Vector3 velocity;
    private float yVelocity;


    private void Update()
    {
        moveDirection = transform.right * Horizontal + transform.forward * Vertical;

        Walk();

        Run();

        Jump();

        Crouch();

        UpdateInAir();

        Move();

    }


    private void Walk()
    {
        velocity = moveSpeed * moveDirection;
    }
    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Vertical > 0)
        {
            velocity = runSpeed * moveDirection;
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }
    private void UpdateInAir()
    {
        if (!cc.isGrounded)
        {
            yVelocity -= gravity * Time.deltaTime;
        }
        else if (yVelocity <= 0)
        {
            yVelocity = -0.3f;
            isJumping = false;
            anim.SetBool("isJumping",false );
        }

        velocity.y = yVelocity;
    }

    private bool CanJump()
    {
        return cc.isGrounded;
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanJump())
        {
            yVelocity = jumpSpeed;
            if (!isJumping)
            {
                isJumping = true;
                anim.SetBool("isJumping", true);
            }
        }
    }
    private void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl) && !isJumping)
        {
            velocity = crouchSpeed * moveDirection;
            if (!isCrouching)
            {
                isCrouching = true;
                anim.SetBool("isCrouching", true);
            }
        }
        else if(isCrouching)
        {
            isCrouching = false;
            anim.SetBool("isCrouching", false);
        }
    }
    private void Move()
    {
        cc.Move(velocity * Time.deltaTime);
        anim.SetFloat("InputX", Horizontal);
        anim.SetFloat("InputY", Vertical);
    }
}
