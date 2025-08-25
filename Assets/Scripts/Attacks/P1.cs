using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class P1 : MonoBehaviour //Inherets the Hitbox class as well as its own code
{

    private Animator anim1; //Animation transition variable 
    private Rigidbody2D rb1;
    public int Jump_force1;
    public bool grounded1;
    NewBehaviourScript ishit; //Reference to Player_movement to tell it that we cannot move if we get hit
    [SerializeField] HurtBox dead;
    [SerializeField] HurtBox other_player;
    
    void Start()
    {
        anim1 = GetComponent<Animator>();
        rb1 = GetComponent<Rigidbody2D>(); 
        ishit = GetComponentInParent<NewBehaviourScript>();
    }




    void Update()
    {
        anim1.SetBool("isJumping1",!grounded1);
    }



    public void OnKick2(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !ishit.ishit)
        {
            if (grounded1)
        {
            anim1.SetTrigger("kick_trigger1");
        }
        }
    }

    public void OnPunch2(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !ishit.ishit)
        {
            if (grounded1)
        {
            Debug.Log("Punch1");
            anim1.SetTrigger("punch_trigger1");
        }
        }
        
        
    }

    public void OnJump1(InputAction.CallbackContext ctx)
    {
       
        if (ctx.performed && !ishit.ishit)
        {
            if (grounded1 && !dead.isdead) //If we are currently on the Ground
        {   
            Debug.Log("Jump1");
            rb1.AddForce(Vector2.up * Jump_force1, ForceMode2D.Impulse); //Uses the rigid body component and adds an upward force with vector2up.jumpForce
        }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other) //When we enter the ground
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            grounded1 = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) //When we leave the ground
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            grounded1 = false;
        }
    }


    private void cannot_jump()
    {
        grounded1 = false;
    }

    private void can_jump()
    {
        grounded1 = true;
    }

   
}
