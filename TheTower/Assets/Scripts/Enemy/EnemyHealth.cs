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
    public Action<Vector2, Action<bool>> OnDeath;

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
         spawner.CurrentEnemyQuantity -= 1;
    }

    public void OnUnpool()
    {
        Health = baseHealth;
        OnHealthChanged += CheckAliveState;

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

            return;
        }

        if (OnDeath != null)
            OnDeath.Invoke(Vector2.right , success =>
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
