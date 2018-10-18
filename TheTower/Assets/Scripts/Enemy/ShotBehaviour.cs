using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehaviour : MonoBehaviour {
	
    [SerializeField] float vel;
    [SerializeField] float damage;
    private SpriteRenderer sprite;
    private Transform leftLimit;
    private Transform rightLimit;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        leftLimit = GameObject.FindGameObjectWithTag("leftLimit").transform;
        rightLimit = GameObject.FindGameObjectWithTag("rightLimit").transform;
        sprite = GetComponent<SpriteRenderer>();

        if (player.transform.position.x < transform.position.x)
        {
            vel = vel * -1;
        }
        else
        {
            sprite.flipX = true;
        }
    }

    void Update ()
    {
        transform.Translate(vel * Time.deltaTime, 0, 0);

        if (transform.position.x > rightLimit.position.x)
            Destroy(gameObject);
        else if (transform.position.x < leftLimit.position.x)
            Destroy(gameObject);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            collision.transform.GetComponent<PlayerHP>().TakeEnemyDamage(damage);
    }

}
