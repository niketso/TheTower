using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerBehaviour : EnemyBehaviour, iPoolable
{
    public float minDistance;

    public ElevatorBehaviour elevatorToBlock;

    protected override void Start()
    {
        base.Start();

        if (!pooled) 
        {
            if (!elevatorToBlock)
                elevatorToBlock = GameManager.instance.Elevators[MyLevel];

            elevatorToBlock.ChangeBlockedState(true);
        }

    }

    private void Update()
    {
        if (!active) return;

        float distance = (player.transform.position - transform.position).magnitude;

        if (distance < minDistance) 
        {
            if (player.transform.position.x < transform.position.x)
                GoRight();
            else
                GoLeft();
        }

        anim.SetBool("canMove" , true);
        CheckBounds();
    }

    private void OnDestroy()
    {
        if(elevatorToBlock)
            elevatorToBlock.ChangeBlockedState(false); 
    }

    public void OnUnpool()
    {
        OnMyLevelChange += LoadElevator;
    }

    public void LoadElevator() 
    {
        elevatorToBlock = GameManager.instance.Elevators[MyLevel];

        elevatorToBlock.ChangeBlockedState(true);
    }

    public void OnPool()
    {
        OnMyLevelChange -= LoadElevator;
        elevatorToBlock.ChangeBlockedState(false);
    }
}
