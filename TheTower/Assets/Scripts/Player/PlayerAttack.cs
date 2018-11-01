using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private float timeAttack;
    private Animator anim;
    [SerializeField] private float damage;
    [SerializeField] private float startTimeAttack;
    [SerializeField] private Transform attackPos;
    [SerializeField] private float range;
    [SerializeField] private LayerMask whatIsEnemy;

    private bool isAttacking = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        SetAttack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, range);
    }


    private void SetAttack()

    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetBool("attacking", true);
            isAttacking = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isAttacking)
            attackPos.localPosition = new Vector2(-1.18f, 0);

        if (Input.GetKeyDown(KeyCode.RightArrow) && !isAttacking)
            attackPos.localPosition = new Vector2(1.18f, 0);
    }
    private void Attack()
    {

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, range, whatIsEnemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].CompareTag("Enemy"))
            {
                enemies[i].GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }
        Debug.Log("Player attacked");
    }

    private void EndAttack()
    {
        isAttacking = false;
        anim.SetBool("attacking", false);
    }
    private void StartAttack()
    {
        isAttacking = true;
    }
}
