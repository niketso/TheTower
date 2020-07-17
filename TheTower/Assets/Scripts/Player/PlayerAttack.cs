using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private float timeAttack;
    private Animator anim;
    [SerializeField] private float damage;
    [SerializeField] private float startTimeAttack;
    [SerializeField] private Transform attackPos;
    [SerializeField] private float range;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private AudioClip attackSound;
    private AudioSource audSource;

    private bool isPaused = false;
    private bool isAttacking = false;
    private bool allowInput = true;

    public float Damage 
    {
        get => damage;
        set => damage = value;
    }

    public bool IsPaused
    {
        get { return isPaused; }
        set { isPaused = value; }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(!isPaused)
            SetAttack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, range);
    }


    private void SetAttack()
    {
        if (allowInput)
        { 
            if (Input.GetKeyDown(KeyCode.X))
            {
                anim.SetBool("attacking", true);
                isAttacking = true;
            }
        
            if (Input.GetKey(KeyCode.LeftArrow))
                attackPos.localPosition = new Vector2(-1.18f, 0);

            if (Input.GetKey(KeyCode.RightArrow))
                attackPos.localPosition = new Vector2(1.18f, 0);
        }
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

    public void DeactivateAllowInput()
    {
        allowInput = false;
    }

    public void ActivateInput()
    {
        allowInput = true;
    }

    public void PlayAttackSound()
    {
        audSource.clip = attackSound;
        audSource.Play();
    }
}
