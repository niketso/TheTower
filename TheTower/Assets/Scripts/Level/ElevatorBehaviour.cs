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
    public int myLevel;

    static public Action OnElevatorStart;
    static public Action OnElevatorFinish;

    private bool active;
    private Animator anim;
    private Collider2D collider;
    private GameObject target;

    private int blockersCount;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();

        active = false;
        target = null;
    }

    private void Start()
    {
        GameManager.instance.Elevators.Add(myLevel , this);
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
        if (blocked)
            blockersCount += 1;
        else 
        {
            blockersCount -= 1;

            if (blockersCount > 0)
                return;
        }
        
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
        // Play audio
        AudioManager.instance.Play("Elevator", false);
    }
}