﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    private void Awake()
    {
        if (instance && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

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
    public int CurrentEnemyQuantity { get => currentEnemyQuantity; set => currentEnemyQuantity = value; }

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
        health.Spawner = this;
        health.strenghtenEnemy(EnemyStrengthPerLevel[GameManager.instance.CurrentFloor]);
        
        behaviour = go.GetComponent<EnemyBehaviour>();
        behaviour.RightLimit = rightLimit;
        behaviour.LeftLimit = leftLimit;

        internalTimer = spawnRate;
    }

    private void SpawnSpecialEnemy(Vector3 position) 
    {
        // Spawn special enemy
    }
}
