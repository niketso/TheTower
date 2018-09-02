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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, range, toDamage);

        if (hit)
        {
            anim.SetBool("canAttack", true);
            
        }
	}
}
