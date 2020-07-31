using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour {

    private Animator anim;
    private Collider2D col;
    private EnemyBehaviour behaviour;

    private bool invulnerable;
    public bool Invulnerable { get => invulnerable; set => invulnerable = value; }

    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private LayerMask toDamage;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        behaviour = GetComponent<EnemyBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("canAttack", true);
            col = collision;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("canAttack", true);
        }
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
                col.GetComponent<PlayerHP>().TakeDamage(behaviour.MyType);
    }

    public void PlayAttackSound()
    {
        //audSource.Play();
        AudioManager.instance.Play("RobotSwordSwing");
    }

    public void ToggleInvulnerability()
    {
        invulnerable = !invulnerable;
    }
}
