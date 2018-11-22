using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MeleeBehaviour : MonoBehaviour {

    private GameObject player;
    private SpriteRenderer sprite;
    private Animator anim;
    private Transform rightLimit;
    private Transform leftLimit;
    [SerializeField] private float speed;

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

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    private void Update ()
    {
        if (player.transform.position.x > transform.position.x )
            GoRight();
        if (player.transform.position.x < transform.position.x)
            GoLeft();
        anim.SetBool("canMove", true);
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

    private void GoLeft()
    {
                
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0f , 0f ) );
        sprite.flipX = false;
    }

    private void GoRight()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0f, 0f) );
        sprite.flipX = true;
    }
}
