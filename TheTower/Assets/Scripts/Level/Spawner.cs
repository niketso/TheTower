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

        Vector3 spawnPos = player.position;
        Transform rightLimit = BoundaryManager.instance.RightLimits[GameManager.instance.CurrentFloor];
        Transform leftLimit = BoundaryManager.instance.LeftLimits[GameManager.instance.CurrentFloor];
        EnemyBehaviour behaviour;
        EnemyHealth health;

        do 
        {
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

        } while (spawnPos.x > leftLimit.position.x && spawnPos.x < rightLimit.position.x);

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








/*
 [SerializeField] private GameObject meleeEnemy;
    [SerializeField] private GameObject enemyHolder;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform spawnPointLeft;
    [SerializeField] private Transform spawnPointRight;
    [SerializeField] private float spawnRate;
    [SerializeField] private BoundaryManager boundaryManager;
    [SerializeField] private float strength;
    [SerializeField] private int floorNumber;
    [SerializeField] private AudioClip hitEnemy;
    [SerializeField] private AudioClip destroyEnemy;
    private AudioSource clip;

    private float timer;

    public bool playerIsHere = false;
    

    private void Awake()
    {
        timer = spawnRate;
        clip = GetComponent<AudioSource>();
    }

    private void Update()
    {
        SpawnMeleeEnemy();
    }

    public Transform PlayerTransform
    {
        get
        {
            return playerTransform;
        }
    }

     private  void SpawnMeleeEnemy()
    {
        
        if (timer <= 0 && playerIsHere)
        {
            int rand;
            rand = Random.Range(1, 3);
            

            switch (rand)
            {
                case 1:
                    GameObject go = Instantiate(meleeEnemy, spawnPointRight.position, Quaternion.identity, enemyHolder.transform);
                    go.GetComponent<EnemyHealth>().AddLife(strength);
                    go.GetComponent<MeleeBehaviour>().RightLimit = boundaryManager.RightLimits[floorNumber - 1];
                    go.GetComponent<MeleeBehaviour>().LeftLimit = boundaryManager.LeftLimits[floorNumber - 1];
                    go.GetComponent<EnemyHealth>().Spawner = this;
                    
                    //Debug.Log("spawn Right");
                    timer = spawnRate;
                    break;

                case 2:
                    GameObject enemy = Instantiate(meleeEnemy, spawnPointLeft.position, Quaternion.identity, enemyHolder.transform);
                    enemy.GetComponent<EnemyHealth>().AddLife(strength);
                    enemy.GetComponent<MeleeBehaviour>().RightLimit = boundaryManager.RightLimits[floorNumber - 1];
                    enemy.GetComponent<MeleeBehaviour>().LeftLimit = boundaryManager.LeftLimits[floorNumber - 1];
                    enemy.GetComponent<EnemyHealth>().Spawner = this;

                    //Debug.Log("spawn Left");
                    timer = spawnRate;
                    break;
            }
        }
        else
            timer -= Time.deltaTime;
    }

    public void ActivateSpawner()
    {
        playerIsHere = true;
    }

    public void DeactivateSpawner()
    {
        playerIsHere = false;
    }

    public void PlayHitAudio()
    {
        clip.clip = hitEnemy;
        clip.Play();
    }

    public void PlayDeathAudio()
    {
        clip.clip = destroyEnemy;
        clip.Play();
    }
     */
