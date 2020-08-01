using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public float maxSplatOffset;
    
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

        OnDeath -= SpawnSplat;
        OnDeath += SpawnSplat;
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

        AudioManager.instance.Play("RobotSpawn", false);// Play enemy spawn audio

        OnDeath -= SpawnSplat;
        OnDeath += SpawnSplat;

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
            AudioManager.instance.Play("RobotHitNotDestroy", false);// Play damaged (no kill) audio
            return;
        }

        if (OnDeath != null)
            OnDeath.Invoke();

        if (type != EnemyType.RANGED)
        {
            AudioManager.instance.Play("DestroyRobot", false);// Play death audio
        }
        else 
        {
            AudioManager.instance.Play("TurretDestroy", false);// Play turret death audio
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

    private void SpawnSplat() 
    {
        Vector3 pos = new Vector3(UnityEngine.Random.Range(-maxSplatOffset, maxSplatOffset), UnityEngine.Random.Range(-maxSplatOffset, maxSplatOffset), 0.0f);

        GameManager.instance.SplatPool.GetObjectFromPool(transform.position + pos);
    }
}
