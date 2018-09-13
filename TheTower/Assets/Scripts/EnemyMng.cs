using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMng : MonoBehaviour {
    [SerializeField] private GameObject meleePrefab;
    [SerializeField] private GameObject enemyHolder;
    [SerializeField] private GameObject rangedPrefab;
    [SerializeField] private GameObject specialPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerPos;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float spawnRate;
    private float timer;
    private float spriteWidth = 0.8f;
    private static EnemyMng instance = null;

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

    private void Update ()
    {
        SpawnMeleeEnemy();
    }

    public void SpawnSpecial()
    {
        Instantiate(specialPrefab, player.transform.position, Quaternion.identity);
    }

    private void SpawnMeleeEnemy() 
    {
        SpriteRenderer sr = meleePrefab.GetComponent<SpriteRenderer>();
        
        float spawnPointLeft =  mainCamera.ViewportToWorldPoint(Vector3.zero).x - sr.sprite.bounds.extents.x;
        float spawnPointRight = mainCamera.ViewportToWorldPoint(Vector3.one).x + sr.sprite.bounds.extents.x;
        float spawnPointY = playerPos.position.y;

        if(timer <= 0) 
        {
            Instantiate(meleePrefab, new Vector3(spawnPointRight, spawnPointY, 0), Quaternion.identity, enemyHolder.transform);
            Debug.Log("spawn");
            timer = spawnRate;
        }
        else
            timer -= Time.deltaTime;
    }
}
