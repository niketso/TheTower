using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerBehaviour : EnemyBehaviour
{
    public float minDistance;

    public ElevatorBehaviour elevatorToBlock;

    private void Start()
    {
        elevatorToBlock.ChangeBlockedState(true);
    }

    private void Update()
    {
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
        elevatorToBlock.ChangeBlockedState(false); 
    }
}
