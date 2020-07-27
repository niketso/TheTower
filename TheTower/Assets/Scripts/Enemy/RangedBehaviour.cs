using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class RangedBehaviour : MonoBehaviour {

    [SerializeField] private float fireRate;
    [SerializeField] private GameObject shot;
    [SerializeField] private Spawner spawner;
    [SerializeField] private bool isSpawnable = true;
    private AudioSource audSource;
    private float timer;
    private Animator anim;
    bool ready = false;
    private Transform playerPos;

    public Spawner Spawner
    {
        get
        {
            return spawner;
        }

        set
        {
            spawner = value;
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        audSource = GetComponent<AudioSource>();
        timer = fireRate;
        playerPos = GameManager.instance.Player.transform;
    }

    private void Update()
    {
        if (ready)
        { 
            if (timer <= 0)
            {
                TriggerAnimation();
                timer = fireRate;
            }
            else
                timer -= Time.deltaTime;
        }

        ChangeState();
    }

    private void TriggerAnimation()
    {
        if (playerPos.position.x > transform.position.x)
        {
            anim.Play("ShootRight");
        }
        else
        {
            anim.Play("ShootLeft");
        }
    }

    public void shoot()
    {
        Vector3 pos = transform.position;
        pos.y = transform.position.y + 0.5f;

        Instantiate(shot, pos, Quaternion.identity);
    }

    public void isReady()
    {
        ready = true;
    }

    public void TurnOff()
    {
        ready = false;
    }

    public void ChangeState()
    {
       //if(isSpawnable)
       //{
       //      if(playerPos.position.x == this.transform.position.y)
       //      {
       //         isReady();
       //      }
       //      else if(playerPos.position.x != this.transform.position.y)
       //      {
       //         TurnOff();
       //      }
       //} 
       //else
       //{
       //     if(spawner.playerIsHere)
       //     {
       //         isReady();
       //     }
       //     else
       //     {
       //         TurnOff();
       //     }
       //}
    }

    public void PlaySound()
    {
        audSource.Play();
    }
}
