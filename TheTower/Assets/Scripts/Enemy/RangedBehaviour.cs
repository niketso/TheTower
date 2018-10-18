using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class RangedBehaviour : MonoBehaviour {

    [SerializeField] private GameObject shot;
    [SerializeField] private EnemyMng Manager;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void shoot()
    {
        if (Manager.PlayerTransform.position.x > transform.position.x)
        {
            anim.SetBool("playerAtRight", true);
            anim.SetBool("playerAtLeft", false);
        }
        else
        {
            anim.SetBool("playerAtRight", false);
            anim.SetBool("playerAtLeft", true);
        }

        Vector3 pos = transform.position;
        pos.y = transform.position.y + 0.5f;

        Instantiate(shot, pos, Quaternion.identity);
    }
}
