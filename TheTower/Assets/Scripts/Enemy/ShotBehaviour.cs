using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehaviour : MonoBehaviour, iPoolable 
{
	
    [SerializeField] float vel;
    [SerializeField] float damage;
    private SpriteRenderer sprite;
    private Transform leftLimit;
    private Transform rightLimit;
    private GameObject player;

    void Update ()
    {
        transform.Translate(vel * Time.deltaTime, 0, 0);

        if (transform.position.x > rightLimit.position.x)
            GameManager.instance.ShotPool.ReturnToPool(this.gameObject);
        else if (transform.position.x < leftLimit.position.x)
            GameManager.instance.ShotPool.ReturnToPool(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player")) 
        {
            collision.transform.GetComponent<PlayerHP>().TakeEnemyDamage(damage, "Ranged");
            GameManager.instance.ShotPool.ReturnToPool(this.gameObject);
        }
    }

    public void OnUnpool()
    {
        player = GameManager.instance.Player;
        leftLimit = GameManager.instance.LeftLimit;
        rightLimit = GameManager.instance.RightLimit;
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

    public void OnPool()
    {
        player = null;
        leftLimit = null;
        rightLimit = null;
    }
}
