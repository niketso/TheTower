﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class ElevatorBehaviour : MonoBehaviour {

    [SerializeField] private float time;
    [SerializeField] private GameObject nextElevator;
    [SerializeField] private GameObject prompt;
    private float timer;

    public UnityEvent newFloor;

    public float Timer
    {
        get
        {
            return timer;
        }
    }

    private void Awake()
    {
        timer = time;
    }

    void Update()
    {
        if (timer <= 0)
        {
            timer = 0;
        }
        else
            timer -= Time.deltaTime;
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        prompt.SetActive(true);

        if (Input.GetKeyDown(KeyCode.C))
        {
            collision.transform.SetPositionAndRotation(new Vector3(nextElevator.transform.position.x, nextElevator.transform.position.y + 1.5f,0), Quaternion.identity);
            newFloor.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        prompt.SetActive(false);
    }
}
