using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    private Animator anim;
    private Collider2D col;
   // private AudioSource audSource;
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private LayerMask toDamage;

    private void Awake()
    {
        anim = GetComponent<Animator>();
       // audSource = GetComponent<AudioSource>();
    }

    void Update ()
    {

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
                col.GetComponent<PlayerHP>().TakeEnemyDamage(damage, "Melee");
    }

    public void PlayAttackSound()
    {
        //audSource.Play();
        AudioManager.instance.Play("RobotSwordSwing");

    }
}
