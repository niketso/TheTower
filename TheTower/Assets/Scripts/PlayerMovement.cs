using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float dashSpeed;
    private float dashTime;
    [SerializeField] private float startDashTime;
    [SerializeField] private float timeToDash;
    private float timeDash;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        timeDash = timeToDash;
    }

    void Update ()
    {
        float mov = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.position += transform.right * mov;


        if (timeDash <= 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                gameObject.layer = 10;
                timeDash = timeToDash;

                if (Input.GetAxis("Horizontal") > 0)
                    rb.velocity = Vector2.right * dashSpeed;
                if (Input.GetAxis("Horizontal") < 0)
                    rb.velocity = Vector2.left * dashSpeed;
            }
            else
            {
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
}
