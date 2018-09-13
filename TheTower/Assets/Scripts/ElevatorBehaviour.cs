using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorBehaviour : MonoBehaviour {

    [SerializeField] private float time;
    [SerializeField] private float gravityForce;
    private Collider2D coll;
    private float timer;

    public float Timer
    {
        get
        {
            return timer;
        }
    }

    private void Awake()
    {
        timer = time;
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (timer <= 0)
        {
            coll.isTrigger = true;
            timer = 0;
        }
        else
            timer -= Time.deltaTime;
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.GetComponent<Rigidbody2D>().gravityScale = -gravityForce;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<Rigidbody2D>().gravityScale = 9.8f;
    }
}
