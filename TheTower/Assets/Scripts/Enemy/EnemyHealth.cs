using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    MELEE, SPECIAL, BLOCKER, COUNT
}

public class EnemyHealth : MonoBehaviour, iPoolable
{
    public EnemyType type;
    public float baseHealth;
    private float health;
    public Action<float> OnHealthChanged;
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

    public void OnPool()
    {
        OnHealthChanged -= CheckAliveState;
        spawner.CurrentEnemyQuantity -= 1;
    }

    public void OnUnpool()
    {
        Health = baseHealth;
        OnHealthChanged += CheckAliveState;
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
        if (!Spawner) return;

        if (health > 0) 
        {//spawner.PlayHitAudio();
            return;
            
        }

        //spawner.PlayDeathAudio();

        switch (type) 
        {
            case EnemyType.MELEE:
                Despawn();
                break;
            case EnemyType.SPECIAL:
                // Ask spawner if it should be returned to pool first, then act acordingly
                Destroy(this.gameObject);
                break;
            case EnemyType.BLOCKER:
                Destroy(this.gameObject);
                break;
            default:
                Debug.LogError($"{gameObject.name}::EnemyHealth::CheckAliveState::Exeption found");
                break;
        }
    }

    private void Despawn(int level = 0)
    {
        GameManager.instance.MeleePool.ReturnToPool(this.gameObject);
    }
}
