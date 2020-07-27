using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSpawner : MonoBehaviour {

	[SerializeField] private Transform playerTransform;
	[SerializeField] private GameObject specialEnemy;
	[SerializeField] private GameObject enemyHolder;
	[SerializeField] private Spawner _spawner;
    [SerializeField] private AudioClip destroyTurret;
    private AudioSource audSource;

	private Transform specialSpawnPoint;

    private void Awake()
    {
        audSource = GetComponent<AudioSource>();
    }

    public  void SpawnSpecial()
    {
        //specialSpawnPoint = playerTransform.GetComponent<PlayerHP>().PlayerDeathPos;
        //GameObject go = Instantiate(specialEnemy, specialSpawnPoint.position, Quaternion.identity);
		//go.GetComponent<RangedBehaviour>().Spawner = _spawner;
        //go.GetComponent<EnemyHealth>().SpecialSpawner = this;
    }

    public void PlayDeathAudio()
    {
        audSource.clip = destroyTurret;
        audSource.Play();
    }
}
