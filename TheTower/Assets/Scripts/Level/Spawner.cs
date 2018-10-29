using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField] private GameObject meleeEnemy;
    [SerializeField] private GameObject enemyHolder;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform spawnPointLeft;
    [SerializeField] private Transform spawnPointRight;
    [SerializeField] private float spawnRate;
    private float timer;

    public bool playerIsHere = false;
    

    private void Awake()
    {
        timer = spawnRate;
    }

    private void Update()
    {
        SpawnMeleeEnemy();
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
                    Instantiate(meleeEnemy, spawnPointRight.position, Quaternion.identity, enemyHolder.transform);
                    //Debug.Log("spawn Right");
                    timer = spawnRate;
                    break;
                case 2:
                    Instantiate(meleeEnemy, spawnPointLeft.position, Quaternion.identity, enemyHolder.transform);
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


}
