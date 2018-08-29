using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] private float health;
    private SpriteRenderer spRend;

    private void Awake()
    {
        spRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        spRend.color = Color.red;
        health -= damage;
        Debug.Log("Enemy Damaged");
    }

    private void Update()
    {
        spRend.color = Color.white;

        if (health <= 0)
            gameObject.SetActive(false);
    }
}
