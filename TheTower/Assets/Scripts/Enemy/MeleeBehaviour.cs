using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBehaviour : MonoBehaviour {

    private GameObject player;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update ()
    {
        if (player.transform.position.x > transform.position.x)
            GoRight();
        if (player.transform.position.x < transform.position.x)
            GoLeft();
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
