using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMng : MonoBehaviour {

    [SerializeField] private GameObject meleePrefab;
    [SerializeField] private GameObject enemyHolder;
    [SerializeField] private GameObject rangedPrefab;
    [SerializeField] private GameObject specialPrefab;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float spawnRate;
    private Camera mainCamera;
    private float timer;
    //private float spriteWidth = 0.8f;
    private static EnemyMng instance = null;
    private Transform specialSpawnPoint;


    public static EnemyMng Instance
    {
        get
        {
            return instance;
        }
    }

    

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        timer = spawnRate;

        
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update ()
    {
        SpawnMeleeEnemy();
        
    }

    public void SpawnSpecial()
    {
        specialSpawnPoint = playerTransform.GetComponent<PlayerHP>().PlayerDeathPos;
        Instantiate(specialPrefab, specialSpawnPoint.position, Quaternion.identity);
        
    }

    private void SpawnMeleeEnemy() 
    {
        SpriteRenderer sr = meleePrefab.GetComponent<SpriteRenderer>();
        
        float spawnPointLeft =  mainCamera.ViewportToWorldPoint(Vector3.zero).x - sr.sprite.bounds.extents.x;
        float spawnPointRight = mainCamera.ViewportToWorldPoint(Vector3.one).x + sr.sprite.bounds.extents.x;
        float spawnPointY = playerTransform.position.y;

        
        if(timer <= 0) 
        {
            int rand;
            rand = Random.Range(1, 3);
            Debug.Log(rand);

            switch (rand)
            {
                case 1:
                    Instantiate(meleePrefab, new Vector3(spawnPointRight, spawnPointY, 0), Quaternion.identity, enemyHolder.transform);
                    Debug.Log("spawn Right");
                    timer = spawnRate;
                    break;
                case 2:
                    Instantiate(meleePrefab, new Vector3(spawnPointLeft, spawnPointY, 0), Quaternion.identity, enemyHolder.transform);
                    Debug.Log("spawn Left");
                    timer = spawnRate;
                    break;
            }
           
            
            
        }
        else
            timer -= Time.deltaTime;
    }
}
