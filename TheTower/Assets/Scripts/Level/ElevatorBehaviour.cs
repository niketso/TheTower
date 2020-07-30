using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.Events;
using System;

public class ElevatorBehaviour : MonoBehaviour 
{
    public Transform transportPoint;
    public GameObject prompt;

    static public Action OnElevatorStart;
    static public Action OnElevatorFinish;

    private bool active;
    private Animator anim;
    private Collider2D collider;
    private GameObject target;

    private bool isBlocked;
    public bool IsBlocked 
    {
        get => isBlocked;
        set => isBlocked = value;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();

        active = false;
        target = null;
    }

    private void Update()
    {
        if (active || !target) return;

        if (Input.GetKeyDown(KeyCode.C)) 
        {
            TriggerElevator();
        }
    }

    public void ChangeBlockedState(bool blocked)
    {
        collider.enabled = !blocked;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target) return;

        prompt.SetActive(true);

        if (collision.CompareTag("Player"))
            target = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (target && collision.gameObject == target) 
        {
            target = null;
            prompt.SetActive(false);
        }
    }

    private void TriggerElevator() 
    {
        anim.SetTrigger("Active");
        
        if (OnElevatorStart != null)
            OnElevatorStart.Invoke();
        
        active = true;
    }

    public void TransportPlayer() 
    {
        target.transform.SetPositionAndRotation(new Vector3(transportPoint.transform.position.x , transportPoint.transform.position.y - 0.5f , 0) , Quaternion.identity);
        GameManager.instance.ChangeFloor(GameManager.instance.CurrentFloor + 1);

        if (OnElevatorFinish != null)
            OnElevatorFinish.Invoke();

        active = false;
    }

    public void PlayElevatorSound() 
    {
        
    }
}









/*
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

        Interact(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Interact(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        prompt.SetActive(false);
        
        anim.SetBool("Active", false);
        
        if (player)
            player.GetComponent<PlayerHP>().Invulnerable = false;
    }

    public void Interact(GameObject go) 
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            player = go;
            player.GetComponent<PlayerHP>().Invulnerable = true;
            
            elevatorActive.Invoke();
        }
    }

    public void TransportPlayer()
    {
        if (player != null)
        {
            

            GameManager.instance.ChangeFloor(GameManager.instance.CurrentFloor + 1);

            player.GetComponent<PlayerHP>().Invulnerable = false;
        }
        else
            Debug.LogError("There's no player to Transport!");
    }

    public void PlayElevatorSound()
    {
        audSource.Play();
    }
*/