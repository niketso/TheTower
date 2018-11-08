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

    [SerializeField] private float strength;
    
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
                    //Debug.Log("spawn Right");
                    timer = spawnRate;
                    break;
                case 2:
                    GameObject enemy = Instantiate(meleeEnemy, spawnPointLeft.position, Quaternion.identity, enemyHolder.transform);
                    enemy.GetComponent<EnemyHealth>().AddLife(strength);

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
