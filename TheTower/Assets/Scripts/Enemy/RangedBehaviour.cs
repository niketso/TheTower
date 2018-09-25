using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBehaviour : MonoBehaviour {

    [SerializeField] private float fireRate;
    [SerializeField] private GameObject shot;
    float timer;

    private void Awake()
    {
        timer = fireRate;
    }

    void Update ()
    {
        if (timer <= 0)
        {
            Instantiate(shot, transform.position, Quaternion.identity);
            timer = fireRate;
        }
        else
            timer -= Time.deltaTime;
	}
}
