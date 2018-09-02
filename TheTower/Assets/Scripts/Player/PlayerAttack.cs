using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private float timeAttack;
    [SerializeField] private float damage;
    [SerializeField] private float startTimeAttack;
    [SerializeField] private Transform attackPos;
    [SerializeField] private float range;
    [SerializeField] private LayerMask whatIsEnemy;

    void Update()
    {
        if (timeAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.X))
                Attack();

            timeAttack = startTimeAttack;
        }
        else
            timeAttack -= Time.deltaTime;
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, range);
    }

    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, range, whatIsEnemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyHealth>().TakeDamage(damage);
        }
        Debug.Log("Player attacked");
    }
}
