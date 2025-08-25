using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;


public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private float moveInput;
    public bool can_move;
    private Animator anim; //Animation transition variable 
    private bool grounded;
    public string horizontal;
    [SerializeField] HurtBox dead;
  
    public bool ishit;
    //Start is called before the first frame update
    void Start()
    {
        
        can_move = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ishit = false;
    }

    // Update is called once per frame
    void Update()
    {   
        if (can_move && grounded && !ishit){
            StartCoroutine(move_delay());
            moveInput = Input.GetAxisRaw(horizontal);
            if (moveInput == 0 || dead.isdead){
                anim.SetBool("walking_state", false);
                rb.velocity = new Vector2(0,rb.velocity.y);
            }

            else{
                
                anim.SetBool("walking_state", true);
                rb.velocity = new Vector2(moveInput * speed, rb.velocity.y); //Allows character to move
            } 
        }
        
        } 

    void move()
    {
        can_move = true;  
    }

    void cannot_move()
    {
        can_move = false;
        // rb.velocity = new Vector2(0,0);
    }

    IEnumerator move_delay()
    {
        yield return new WaitForSeconds(1f);
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

}
