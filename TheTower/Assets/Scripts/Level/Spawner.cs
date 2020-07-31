using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnRate;
    [SerializeField] private int maxEnemiesSpawned;
    public float enemySpawnMinRange;
    public float enemySpawnMaxRange;
    private int currentEnemyQuantity;
    private float internalTimer;
    private Transform player;

    private ObjectPool meleePool;
    private ObjectPool specialPool;

    public List<int> EnemyStrengthPerLevel;
    public List<int> LevelsWithoutEnemies;

    public int MaxEnemiesSpawned 
    { 
        get => maxEnemiesSpawned; 
        set => maxEnemiesSpawned = value; 
    }
    public int CurrentEnemyQuantity 
    { 
        get => currentEnemyQuantity; 
        set 
        {
            currentEnemyQuantity = value;

            if (currentEnemyQuantity < 0) currentEnemyQuantity = 0;
        }
    }

    private void Start()
    {
        player = GameManager.instance.Player.transform;
        meleePool = GameManager.instance.MeleePool;
        specialPool = GameManager.instance.SpecialPool;

        CurrentEnemyQuantity = 0;

        internalTimer = spawnRate;
    }

    private void Update()
    {
        if (internalTimer <= 0)
            SpawnEnemy();
        else
            internalTimer -= Time.deltaTime;
    }

    private void SpawnEnemy() 
    {
        if (CurrentEnemyQuantity >= MaxEnemiesSpawned) return;

        if (!player)
            player = GameManager.instance.Player.transform;

        if (!player) return;

        foreach (int level in LevelsWithoutEnemies) 
        {
            if (level == GameManager.instance.CurrentFloor)
                return;
        }

        int maxAttempts = 4;
        int currentAttempts = 0;

        Vector3 spawnPos = Vector3.zero;
        Transform rightLimit = BoundaryManager.instance.RightLimits[GameManager.instance.CurrentFloor];
        Transform leftLimit = BoundaryManager.instance.LeftLimits[GameManager.instance.CurrentFloor];
        EnemyBehaviour behaviour;
        EnemyHealth health;

        do 
        {
            spawnPos = player.position;
            float offset = Random.Range(enemySpawnMinRange, enemySpawnMaxRange);
            int side = Random.Range(1, 3);

            switch (side) 
            {
                case 1:
                    spawnPos.x -= offset;
                    break;
                case 2:
                    spawnPos.x += offset;
                    break;
            }

            if (currentAttempts >= maxAttempts)
                return;
            else
                currentAttempts++;

        } while (spawnPos.x < leftLimit.position.x || spawnPos.x > rightLimit.position.x);

        GameObject go = meleePool.GetObjectFromPool(spawnPos);
        CurrentEnemyQuantity += 1;

        health = go.GetComponent<EnemyHealth>();
        health.strenghtenEnemy(EnemyStrengthPerLevel[GameManager.instance.CurrentFloor]);
        health.pooled = true;

        behaviour = go.GetComponent<EnemyBehaviour>();
        behaviour.RightLimit = rightLimit;
        behaviour.LeftLimit = leftLimit;

        internalTimer = spawnRate;
    }

    public void SpawnSpecialEnemy(Vector3 position) 
    {
        GameObject go = GameManager.instance.SpecialPool.GetObjectFromPool(position);

        EnemyHealth health = go.GetComponent<EnemyHealth>();
        EnemyBehaviour behaviour = go.GetComponent<EnemyBehaviour>();

        behaviour.RightLimit = BoundaryManager.instance.RightLimits[GameManager.instance.CurrentFloor];
        behaviour.LeftLimit = BoundaryManager.instance.LeftLimits[GameManager.instance.CurrentFloor];
        behaviour.MyLevel = GameManager.instance.CurrentFloor;

        health.pooled = true;
    }
}
