using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour, iPoolable
{

    private Animator anim;
    private Collider2D col;
    private EnemyBehaviour behaviour;
    private EnemyHealth health;

    private bool invulnerable;
    public bool Invulnerable { get => invulnerable; set => invulnerable = value; }

    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private LayerMask toDamage;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        behaviour = GetComponent<EnemyBehaviour>();
        health = GetComponent<EnemyHealth>();
    }

    public void OnPool() 
    {
    }

    public void OnUnpool() 
    {
        invulnerable = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!behaviour.active) return;

        if (collision.CompareTag("Player") && !invulnerable)
        {
            anim.SetBool("canAttack", true);
            col = collision;
            ToggleInvulnerability();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!behaviour.active) return;

        if (collision.CompareTag("Player") && !invulnerable)
        {
            anim.SetBool("canAttack", true);
            ToggleInvulnerability();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetBool("canAttack", false);
        col = null;
    }

    public void Damage()
    {
        if (!behaviour.active) return;

        ToggleInvulnerability();

        if (col != null)
            if (col.transform.CompareTag("Player"))
                col.GetComponent<PlayerHP>().TakeDamage(behaviour.MyType);
    }

    public void PlayAttackSound()
    {
        AudioManager.instance.Play("RobotSwordSwing",false);
    }

    public void ToggleInvulnerability()
    {
        invulnerable = !invulnerable;
        Debug.Log($"{gameObject.name}::{invulnerable}");
    }
}
