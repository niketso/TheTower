using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorBehaviour : MonoBehaviour {

    [SerializeField] private float time;
    private float timer;
    private Animator anim;

    public float Timer
    {
        get
        {
            return timer;
        }
    }

    private void Awake()
    {
        timer = time;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (timer <= 0)
        {
            timer = 0;
            anim.SetBool("Active", true);
        }
        else
            timer -= Time.deltaTime;
	}
}
