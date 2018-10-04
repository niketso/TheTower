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

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        /*
            Para activar las animaciones hay que sacar el llamado a la funcion "Attack" en la deteccion de la tecla X y descomentar la linea que esta arriba del if 
            y la que esta abajo del llamado a la funcion attack 
        */

        //anim.SetBool("attacking", false);
        /*if (timeAttack <= 0)
        {*/
            if (Input.GetKeyDown(KeyCode.X))
            {
                anim.SetBool("attacking",true);
            }
               

           /* timeAttack = startTimeAttack;
        }/*
       /* else
        {
            timeAttack -= Time.deltaTime;
        }*/

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            attackPos.localPosition = new Vector2 (-1.18f, 0);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            attackPos.localPosition = new Vector2(1.18f, 0);
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

    private void EndAttack()
    {
        anim.SetBool("attacking", false);
    }
}
