using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

	[SerializeField] private float speed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float startDashTime;
    [SerializeField] private float timeToDash;
    [SerializeField] private Transform attack;
    private float dashTime;
    private float timerToNextDash;
	private float lastInput;
    private float timedashing;
    private Animator anim;
    private bool moving = false;
    private SpriteRenderer spriteRend;
    [SerializeField] private Sprite dashSprite;
    [SerializeField] private Sprite idleSprite;



    public float TimerToNextDash
    {
        get
        {
            return timerToNextDash;
        }
  
    }

    private void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        dashTime = startDashTime;
        timerToNextDash = timeToDash;
    }

    void Update ()
    {
		PMov();
        Dash();
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Z) && timerToNextDash <= 0f)
        {
            gameObject.layer = 10;
            //spriteRend.sprite = dashSprite;
            anim.SetBool("dashing",true);
            timerToNextDash = timeToDash;

            timedashing = 0.2f;
        }
        else
        {
            timerToNextDash -= Time.deltaTime;
        }

        if (timedashing > 0.0f)
        {
            if (lastInput > 0)
                transform.Translate(Vector3.right * dashSpeed * Time.deltaTime);
            if (lastInput < 0)
                transform.Translate(Vector3.left * dashSpeed * Time.deltaTime);

            timedashing -= Time.deltaTime;
        }
        else
        {
            gameObject.layer = 9;
            // anim.SetTrigger("idle");
            anim.SetBool("dashing", false);
            if (moving)
            {
                anim.SetTrigger("running");
            }
            else
                anim.SetTrigger("idle");
            //spriteRend.sprite = idleSprite;
            transform.Translate(Vector3.zero);
        }

    }
        /* if (timerToNextDash <= 0f) {
         if (Input.GetKeyDown(KeyCode.Z)) {

             gameObject.layer = 10;
             spriteRend.sprite = dashSprite;
             timerToNextDash = TimeToDash;

             if (lastInput > 0)
                 transform.Translate +=  * dashSpeed;
             if (lastInput < 0)
                  transform.position += Vector3.left * dashSpeed;
         }
         else {
             if (dashTime <= 0)
             {
                 dashTime = startDashTime;
                 //transform.position = Vector2.zero;
                 gameObject.layer = 9;
                 spriteRend.sprite = idleSprite;
             }
             else
                 dashTime -= Time.deltaTime;
         }
     }
     else
         timerToNextDash -= Time.deltaTime;
        }*/
    

	private void PMov()
	{
		float mov = Input.GetAxis("Horizontal")* speed * Time.deltaTime;
		transform.position += transform.right * mov;


        if (mov != 0f)
        {
            moving = true;
            lastInput = mov;
            anim.SetTrigger("running");
        }
        else
        {
            moving = false;
            anim.SetTrigger("idle");

        }
            

        if (lastInput > 0f)
            spriteRend.flipX = false;
        if (lastInput < 0f)
            spriteRend.flipX = true;
    }
}
