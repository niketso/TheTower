using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class RangedBehaviour : EnemyBehaviour 
{
    public float timeBetweenAttacks;
    private float internalTimer;

    public Transform gunpoint;

    private Vector3 cachedPosition;

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

        cachedPosition = player.transform.position;
    }

    public void Shoot() 
    {
        ShotBehaviour shot = GameManager.instance.ShotPool.GetObjectFromPool(gunpoint.position).GetComponent<ShotBehaviour>();
        shot.Setup(cachedPosition);

        internalTimer = timeBetweenAttacks;
    }

    public override void PlaySound()
    {
        AudioManager.instance.Play("TurretChargeAndShoot", false);
    }
}
