using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] private float _health;
    private float _strength;
    private SpriteRenderer spRend;

    public float Health
    {
        get
        {
            return _health;
        }
   
    }

    private void Awake()
    {
        spRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        spRend.color = Color.red;
        _health -= damage;
        Debug.Log("Enemy Damaged");
    }

    private void Update()
    {
        spRend.color = Color.white;

        if (Health <= 0)
            Destroy(gameObject);
    }

    public void AddLife(float strength)
    {
        _strength = strength;
        _health += strength;
    }

    public float GetStrength()
    {
        return _strength;
    }
}
