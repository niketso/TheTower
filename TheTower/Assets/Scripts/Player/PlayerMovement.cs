﻿using System.Collections;
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
    private float timeDash;
	private float lastInput;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    public float TimeToDash
    {
        get
        {
            return timeToDash;
        }
  
    }

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        timeDash = TimeToDash;
    }

    void Update ()
    {
		PMov();
    }

	private void FixedUpdate() 
	{
		Dash();
	}

	private void Dash() 
	{
		if (timeDash <= 0) {
			if (Input.GetKeyDown(KeyCode.Z)) {
				gameObject.layer = 10;
				timeDash = TimeToDash;

                if (lastInput > 0)
                    rb.velocity = Vector2.right * dashSpeed;
				if (lastInput < 0)
					rb.velocity = Vector2.left * dashSpeed;
			}
			else {
				if (dashTime <= 0)
                {
					dashTime = startDashTime;
					rb.velocity = Vector2.zero;
					gameObject.layer = 9;
				}
				else
					dashTime -= Time.deltaTime;
			}
		}
		else
			timeDash -= Time.deltaTime;
	}

	private void PMov()
	{
		float mov = Input.GetAxis("Horizontal")* speed * Time.deltaTime ;
		transform.position += transform.right * mov;

        if (mov != 0)
			lastInput = mov;

        if (lastInput > 0)
            sprite.flipX = false;
        if (lastInput < 0)
            sprite.flipX = true;
    }
}
