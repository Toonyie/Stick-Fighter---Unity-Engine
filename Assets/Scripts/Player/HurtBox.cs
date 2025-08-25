using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HurtBox : MonoBehaviour
{
    public int health;
    public LayerMask layerMask;
    
    [SerializeField] GameObject[] hearts; //Display health
    [SerializeField] GameObject other_Player; //Reference to see other_Player's health

    [SerializeField] GameObject winner; //Who wins object
    [SerializeField] GameObject button; //Play again object reference
    [SerializeField] GameObject menu; //Quit button object reference
    [SerializeField] GameObject doubleko; //Reference to double ko object

    [SerializeField] Animator anim; //hit animation referenece
    public bool isdead = false;
    [SerializeField] int num; //Player number
    
    Rigidbody2D body;
    NewBehaviourScript ishit; //Reference to Player_movement to tell it that we cannot move if we get hit
    hitstun hit; //histun reference
    HurtBox hurtbox2;

    void Start()
    {
        health = 3;
        body = GetComponentInParent<Rigidbody2D>();
        ishit = GetComponentInParent<NewBehaviourScript>();
        hit = GetComponentInParent<hitstun>();
        hurtbox2 = other_Player.GetComponentInChildren<HurtBox>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 2)
        {
            Destroy(hearts[0].gameObject);
        }
        else if (health == 1)
        {
            Destroy(hearts[1].gameObject);
        }
        else if (health == 0 && !isdead)
        {
            Destroy(hearts[2].gameObject);
            anim.SetTrigger("daed");
            isdead = true;
            Time.timeScale = 0.5f;
            StartCoroutine(roundend(0.2f));
            
            
        }

        
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
     
        if (layerMask == (layerMask | (1 << other.transform.gameObject.layer))) //Checks if the layermask from the trigger is whatever the objects layermask is
        {//In this case check if the attack hitbox collider is at the hurtbox layer
            HitBox h = other.GetComponent<HitBox>(); //Checks if the Hurtbox.cs code is a component in the object
            
            if (h != null)
            {
                health -= 1;
                if (health != 0) //if health is 0, it won't run any hit animations because the player is koed
                {
                    OnHit(other.name); //Runs the OnHit function depending on the attack
                }
                
            }
        }
        
    }

    private void OnHit(string attack) //Function where all the properties of the attack hits take place
    {
        
            switch (attack)
        {
            case "punch_hitbox":
            Debug.Log($"Player {num} hit by punch");

            anim.SetTrigger("high_hit");
            anim.SetBool("is_hit", true);
            ishit.ishit = true;
            switch (num) //Knockback left or right depending on player number
            {
                case 1:
                hit.StopTime(0.05f, 10, 0.5f);
                body.AddForce(Vector2.left * 8, ForceMode2D.Impulse);
                StartCoroutine(hitstun(0.25f));
                break;

                case 2:
                hit.StopTime(0.05f, 10, 0.5f);
                
                body.AddForce(Vector2.right * 8, ForceMode2D.Impulse);
                StartCoroutine(hitstun(0.25f));
                break;
            }
            
            break;

            case "kick_hitbox":
            Debug.Log($"Player {num} hit by kick");
            ishit.ishit = true;
            anim.SetTrigger("low_hit");
            switch (num) //Knockback left or right depending on player number
            {
                case 1:
                hit.StopTime(0.05f, 10, 0.5f);

                body.AddForce(Vector2.left * 8, ForceMode2D.Impulse);
                StartCoroutine(hitstun(0.25f));
                break;

                case 2:
                hit.StopTime(0.05f, 10, 0.5f);
                
                body.AddForce(Vector2.right * 8, ForceMode2D.Impulse);
                StartCoroutine(hitstun(0.25f));
                break;
            }
            
            break;
        
        }
        
    }

    IEnumerator hitstun(float hitstun)
    {
        yield return new WaitForSeconds(hitstun);
        ishit.ishit = false;
        anim.SetBool("is_hit", false);
    }

    IEnumerator roundend(float time)
    {
        yield return new WaitForSeconds(time);
        if (hurtbox2.health == 0)
        {
            doubleko.SetActive(true);
        }
        else
        {
            winner.SetActive(true);
        }
        
        button.SetActive(true);
        menu.SetActive(true);
    }
}
