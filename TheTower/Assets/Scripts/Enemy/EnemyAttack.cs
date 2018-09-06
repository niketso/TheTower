using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    private Animator anim;
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private LayerMask toDamage;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update ()
    {

	}

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        anim.SetBool("canAttack", true);
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        anim.SetBool("canAttack", false);
    }

    private void OnTriggerStay2D(Collider2D collision) 
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyAttack")) 
        {
            if(collision.transform.CompareTag("Player")) 
            {
                collision.GetComponent<PlayerHP>().TakeEnemyDamage(damage);
                Debug.Log("Enemy has attacked");
            }
        }
    }
}
