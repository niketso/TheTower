using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class RangedBehaviour : MonoBehaviour {

    [SerializeField] private float fireRate;
    [SerializeField] private GameObject shot;
    [SerializeField] private EnemyMng Manager;
    private float timer;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        timer = fireRate;
    }

    private void Update()
    {
        if (timer <= 0)
        {
            TriggerAnimation();
            timer = fireRate;
        }
        else
            timer -= Time.deltaTime;
    }

    private void TriggerAnimation()
    {
        if (Manager.PlayerTransform.position.x > transform.position.x)
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
}
