using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public enum EnemyType
{
    MELEE, RANGED, BLOCKER, COUNT
}

public class EnemyHealth : MonoBehaviour, iPoolable
{
    public EnemyType type;
    public float baseHealth;
    private float health;
    
    public Action<float> OnHealthChanged;
    public Action OnDeath;
    public Action OnSpawn;

    public bool pooled = false;

    public float Health 
    {
        get => health;
        private set 
        {
            health = value;

            if (OnHealthChanged != null)
                OnHealthChanged.Invoke(health);
        }
    }

    public Spawner Spawner { get => spawner; set => spawner = value; }

    private Spawner spawner;
    private SpriteRenderer sprite;

    private void Start()
    {
        if (pooled) return;

        Health = baseHealth;
        OnHealthChanged -= CheckAliveState;
        OnHealthChanged += CheckAliveState;
    }

    public void OnPool()
    {
        OnHealthChanged -= CheckAliveState;
        GameManager.instance.OnCurrentFloorChanged -= Despawn;
        
        if(type != EnemyType.BLOCKER)
            Spawner.CurrentEnemyQuantity -= 1;
    }

    public void OnUnpool()
    {
        Health = baseHealth;
        OnHealthChanged += CheckAliveState;

        // Play enemy spawn audio

        if (type != EnemyType.BLOCKER)
            GameManager.instance.OnCurrentFloorChanged += Despawn;
    }

    public void strenghtenEnemy(float strength) 
    {
        Health += strength;
    }

    public void TakeDamage(float damage) 
    {
        Health -= damage;
    }

    private void CheckAliveState(float health) 
    {
        if (health > 0) 
        {
            // Play damaged (no kill) audio
            return;
        }

        if (OnDeath != null)
            OnDeath.Invoke();

        if (type != EnemyType.RANGED)
        {
            // Play death audio
        }
        else 
        {
            // Play turret death audio
        }

        GetComponent<ShaderController>().TriggerDespawn(success => 
        {
            switch (type)
            {
                case EnemyType.MELEE:
                    Despawn();
                    break;
                case EnemyType.RANGED:
                    Destroy();
                    break;
                case EnemyType.BLOCKER:
                    if (pooled)
                        Despawn();
                    else
                        Destroy();
                    break;
                default:
                    Debug.LogError($"{gameObject.name}::EnemyHealth::CheckAliveState::Exeption found");
                    break;
            }
        });
    }

    private void Destroy() 
    {
        Destroy(this.gameObject);
    }

    private void Despawn(int level = 0)
    {
        GameManager.instance.MeleePool.ReturnToPool(this.gameObject);
    }
}
