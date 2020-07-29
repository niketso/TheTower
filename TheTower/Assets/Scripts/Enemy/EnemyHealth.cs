using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] private float _health;
    [SerializeField] private Spawner spawner;
    [SerializeField] private SpecialSpawner specialSpawner;
    private float _strength;
    private SpriteRenderer spRend;

    public float Health
    {
        get { return _health; }
   
    }

    public Spawner Spawner
    {
        get { return spawner; }
        set { spawner = value; }
    }

    public SpecialSpawner SpecialSpawner
    {
        get{ return specialSpawner; }
        set { specialSpawner = value; }
    }

    private void Awake()
    {
        spRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        spRend.color = Color.red;

        _health -= damage;

        if (spawner)
        {
            if (Health <= 0)
            {
                 spawner.PlayDeathAudio();
                //AudioManager.instance.Play("");
                Destroy(gameObject);
            }
            else
            {
                spawner.PlayHitAudio();
               // AudioManager.instance.Play("");
            }
        }
        else if (specialSpawner)
        {
            if (Health <= 0)
            {
                specialSpawner.PlayDeathAudio();
                //AudioManager.instance.Play("");
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        spRend.color = Color.white;

        
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
