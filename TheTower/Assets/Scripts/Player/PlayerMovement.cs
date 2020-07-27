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
    [SerializeField] private AudioClip dashSound;
    private AudioSource audSource;
    private float dashTime;
    private float timerToNextDash;
	private float lastInput;
    private float timedashing;
    private Animator anim;
    private bool moving = false;
    private SpriteRenderer spriteRend;
    private Transform rightLimit;
    private Transform leftLimit;
    private bool canMove;
    private bool isPaused;

    public float TimerToNextDash
    {
        get { return timerToNextDash; }
    }

    public Transform RightLimit
    {
        get { return rightLimit; }
        set { rightLimit = value; }
    }

    public Transform LeftLimit
    {
        get { return leftLimit; }
        set { leftLimit = value; }
    }

    public bool IsPaused
    {
        get{ return isPaused; }
        set{ isPaused = value; }
    }

    private void Awake()
    {
        canMove = true;

        spriteRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audSource = GetComponent<AudioSource>();
        audSource.volume = PlayerPrefs.GetFloat("volume");

        dashTime = startDashTime;
        timerToNextDash = timeToDash;
    }

    void Update()
    {
        if (!isPaused)
        {
            if (canMove)
            { 
                PMov();
            }
            Dash();
            CheckBounds();
        }
    }

    private void CheckBounds()
    {
        if (transform.position.x <= leftLimit.position.x)
        {
            transform.position = new Vector2(leftLimit.position.x, transform.position.y);
        }
        else if (transform.position.x >= rightLimit.position.x)
        {
            transform.position = new Vector2(rightLimit.position.x, transform.position.y);
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Z) && timerToNextDash <= 0f)
        {
            gameObject.layer = 10;
            GetComponent<PlayerHP>().CanBeHit = false;
            anim.SetBool("dashing",true);
            audSource.clip = dashSound;
            audSource.volume = PlayerPrefs.GetFloat("volume");
            audSource.Play();
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
            GetComponent<PlayerHP>().CanBeHit = true;
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

    public void DeactivateCanMove()
    {
        canMove = false;
    }

    public void ActivateCanMove()
    {
        canMove = true;
    }
}
