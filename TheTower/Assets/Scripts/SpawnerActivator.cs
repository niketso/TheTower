using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerActivator : MonoBehaviour {

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            Spawner spwn;
            spwn = this.GetComponentInParent<Spawner>();
            spwn.ActivateSpawner();
        }

    }
}
