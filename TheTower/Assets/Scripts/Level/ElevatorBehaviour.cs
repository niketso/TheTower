﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.Events;


public class ElevatorBehaviour : MonoBehaviour {

    [SerializeField] private GameObject nextElevator;
    [SerializeField] private GameObject prompt;
    private Animator anim;
    private AudioSource audSource;
    private GameObject player;
    private Collider2D collider;

    private bool isBlocked;
    public bool IsBlocked 
    {
        get => IsBlocked;
        set => isBlocked = value;
    }

    public UnityEvent newFloor;
    public UnityEvent elevatorActive;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audSource = GetComponent<AudioSource>();
        collider = GetComponent<Collider2D>();
    }

    public void ChangeBlockedState(bool blocked) 
    {
        if (blocked)
            collider.enabled = false;
        else
            collider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        prompt.SetActive(true);

        
        if (Input.GetKeyDown(KeyCode.C))
        {
            player = collision.gameObject;
            player.GetComponent<PlayerHP>().CanBeHit = false;
            anim.SetBool("Active", true);
            elevatorActive.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            player = collision.gameObject;
            player.GetComponent<PlayerHP>().CanBeHit = false;
            anim.SetBool("Active", true);
            elevatorActive.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        prompt.SetActive(false);
        anim.SetBool("Active", false);
        if (player)
            player.GetComponent<PlayerHP>().CanBeHit = true;
    }

    public void TransportPlayer()
    {
        if (player != null)
        {
            player.transform.SetPositionAndRotation(new Vector3(nextElevator.transform.position.x, nextElevator.transform.position.y - 0.5f, 0), Quaternion.identity);
            newFloor.Invoke();
            player.GetComponent<PlayerHP>().CanBeHit = true;
        }
        else
            Debug.LogError("There's no player to Transport!");
    }

    public void PlayElevatorSound()
    {
        audSource.Play();
    }
}
