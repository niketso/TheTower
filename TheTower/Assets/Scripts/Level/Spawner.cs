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
}
