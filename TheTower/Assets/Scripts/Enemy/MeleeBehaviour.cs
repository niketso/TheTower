using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MeleeBehaviour : MonoBehaviour {

    private GameObject player;
    private SpriteRenderer sprite;
    private Animator anim;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    private void Update ()
    {
        if (player.transform.position.x > transform.position.x)
            GoRight();
        if (player.transform.position.x < transform.position.x)
            GoLeft();
        anim.SetBool("canMove", true);
	}

    private void GoLeft()
    {
        transform.Translate(-0.02f, 0f, 0f);
        sprite.flipX = false;
    }

    private void GoRight()
    {
        transform.Translate(0.02f, 0f, 0f);
        sprite.flipX = true;
    }
}
