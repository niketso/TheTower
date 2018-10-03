using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehaviour : MonoBehaviour {
	
    [SerializeField] float vel;
    [SerializeField] float damage;
    private Transform leftLimit;
    private Transform rightLimit;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        leftLimit = GameObject.FindGameObjectWithTag("leftLimit").transform;
        rightLimit = GameObject.FindGameObjectWithTag("rightLimit").transform;

        if (player.transform.position.x < transform.position.x)
            vel = vel * -1;
    }

    void Update ()
    {
        transform.Translate(vel * Time.deltaTime, 0, 0);

        if (transform.position.x > rightLimit.position.x)
            Destroy(gameObject);
        else if (transform.position.x < leftLimit.position.x)
            Destroy(gameObject);
	}

   /* private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.CompareTag("Player"))
            collision.transform.GetComponent<PlayerHP>().TakeEnemyDamage(damage);
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Break();
        if (collision.transform.CompareTag("Player"))
            collision.transform.GetComponent<PlayerHP>().TakeEnemyDamage(damage);
    }

}
