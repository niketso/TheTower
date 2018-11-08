using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class RangedBehaviour : MonoBehaviour {

    [SerializeField] private float fireRate;
    [SerializeField] private GameObject shot;
    [SerializeField] private Spawner spawner;
    private float timer;
    private Animator anim;
    bool ready = false;

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

    private void Awake()
    {
        anim = GetComponent<Animator>();
        timer = fireRate;
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
        if (spawner.PlayerTransform.position.x > transform.position.x)
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
       if(spawner.PlayerTransform.position.y == this.transform.position.y)
       {
            isReady();
            Debug.Log("torreta Activa");
       }
       else if(spawner.PlayerTransform.position.y != this.transform.position.y)
       {
            TurnOff();
            Debug.Log("torreta off");
       }
    }
}
