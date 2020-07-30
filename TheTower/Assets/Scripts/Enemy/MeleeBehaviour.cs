using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MeleeBehaviour : EnemyBehaviour
{
    protected override void Start()
    {
        base.Start();

        MyType = EnemyType.MELEE;
    }

    private void Update ()
    {
        if (player.transform.position.x > transform.position.x )
            GoRight();
        if (player.transform.position.x < transform.position.x)
            GoLeft();
        anim.SetBool("canMove", true);
        CheckBounds();
    }
}
