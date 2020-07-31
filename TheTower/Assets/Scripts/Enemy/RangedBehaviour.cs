using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class RangedBehaviour : EnemyBehaviour 
{
    public float timeBetweenAttacks;
    private float internalTimer;

    public Transform gunpoint;

    protected override void Start()
    {
        base.Start();

        active = MyLevel == GameManager.instance.CurrentFloor;
        internalTimer = timeBetweenAttacks;
    }

    private void Update()
    {
        if (!active) return;

        if (internalTimer <= 0)
            TriggerAnimation();
        else
            internalTimer -= Time.deltaTime;
    }

    private void TriggerAnimation()
    {
        if (player.transform.position.x > transform.position.x)
        {
            anim.Play("ShootRight");
        }
        else
        {
            anim.Play("ShootLeft");
        }
    }

    public void Shoot() 
    {
        GameManager.instance.ShotPool.GetObjectFromPool(gunpoint.position);

        internalTimer = timeBetweenAttacks;
    }

    public override void PlaySound()
    {
        AudioManager.instance.Play("TurretChargeAndShoot", false);// Play shoot audio
    }
}
