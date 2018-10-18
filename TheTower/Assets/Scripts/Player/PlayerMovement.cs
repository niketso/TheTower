using System;
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
    private Transform rightLimit;
    private Transform leftLimit;


    public float TimerToNextDash
    {
        get
        {
            return timerToNextDash;
        }
  
    }

    public Transform RightLimit
    {
        get
        {
            return rightLimit;
        }

        set
        {
            rightLimit = value;
        }
    }

    public Transform LeftLimit
    {
        get
        {
            return leftLimit;
        }

        set
        {
            leftLimit = value;
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
        CheckBounds();
    }

    private void CheckBounds()
    {
        if (transform.position.x <= rightLimit.position.x)
        {
            transform.position = new Vector2(rightLimit.position.x, transform.position.y);
        }
        else if (transform.position.x >= leftLimit.position.x)
        {
            transform.position = new Vector2(leftLimit.position.x, transform.position.y);
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Z) && timerToNextDash <= 0f)
        {
            gameObject.layer = 10;
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
            anim.SetBool("dashing", false);
            if (moving)
            {
                anim.SetTrigger("running");
            }
            else
                anim.SetTrigger("idle");

            transform.Translate(Vector3.zero);
        }

    }
    

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
