using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MeleeBehaviour : EnemyBehaviour, iPoolable
{
    public void OnPool()
    {
    }

    public void OnUnpool()
    {
        active = true;
    }

    private void Update ()
    {
        if (!active) return;

        if (player.transform.position.x > transform.position.x )
            GoRight();
        if (player.transform.position.x < transform.position.x)
            GoLeft();
        anim.SetBool("canMove", true);
        CheckBounds();
    }
}
