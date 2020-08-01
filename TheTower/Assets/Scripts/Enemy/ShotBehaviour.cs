using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehaviour : MonoBehaviour, iPoolable 
{
	
    public float vel;
    public float damage;
    private float internalVelocity;
    private SpriteRenderer sprite;
    private Transform leftLimit;
    private Transform rightLimit;
    private GameObject player;

    void Update ()
    {
        transform.Translate(internalVelocity * Time.deltaTime, 0, 0);

        if (transform.position.x > rightLimit.position.x)
            GameManager.instance.ShotPool.ReturnToPool(this.gameObject);
        else if (transform.position.x < leftLimit.position.x)
            GameManager.instance.ShotPool.ReturnToPool(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player")) 
        {
            collision.transform.GetComponent<PlayerHP>().TakeDamage(EnemyType.RANGED);
            GoBackToPool();
        }
    }

    public void OnUnpool()
    {
        player = GameManager.instance.Player;
        leftLimit = GameManager.instance.LeftLimit;
        rightLimit = GameManager.instance.RightLimit;
        sprite = GetComponent<SpriteRenderer>();

        GameManager.instance.OnCurrentFloorChanged += GoBackToPool;
    }

    public void Setup(Vector3 pos) 
    {
        if (pos.x < transform.position.x)
        {
            internalVelocity = vel * -1;
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

        GameManager.instance.OnCurrentFloorChanged -= GoBackToPool;
    }

    private void GoBackToPool(int level = 0)
    {
        GameManager.instance.ShotPool.ReturnToPool(this.gameObject);
    }
}
