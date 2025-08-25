using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class P2 : MonoBehaviour //Inherets the Hitbox class as well as its own code
{
    private Animator anim; //Animation transition variable 
    private Rigidbody2D rb;
    public int Jump_force;
    public bool grounded;
    [SerializeField] HurtBox dead;
    NewBehaviourScript ishit; //Reference to Player_movement to tell it that we cannot attack if we get hit

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        anim.SetBool("isJumping1",!grounded);
    }

  


    public void OnKick1(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (grounded)
        {
            Debug.Log("Kick");
            anim.SetTrigger("kick_trigger1");
           
        }
        }
        
        
    }

    public void OnPunch1(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (grounded)
        {
            Debug.Log("Punch");
            anim.SetTrigger("punch_trigger1");
        }

        }
        
        
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            
        if (grounded && !dead.isdead) //If we are currently on the Ground
        {
            Debug.Log("Jump");
            rb.AddForce(Vector2.up * Jump_force, ForceMode2D.Impulse); //Uses the rigid body component and adds an upward force with vector2up.jumpForce
        }

        }
        
    }

    private void OnCollisionEnter2D(Collision2D other) //When we enter the ground
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) //When we leave the ground
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    private void cannot_jump()
    {
        grounded = false;
    }

    private void can_jump()
    {
        grounded = true;
    }
}
