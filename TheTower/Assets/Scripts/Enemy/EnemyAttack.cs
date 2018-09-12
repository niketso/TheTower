using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    private Animator anim;
    private Collider2D col;
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
        col = collision;
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        anim.SetBool("canAttack", false);
        col = null;
    }

    public void Damage()
    {
        if(col != null)
            if (col.transform.CompareTag("Player"))
                col.GetComponent<PlayerHP>().TakeEnemyDamage(damage);
    }
}
