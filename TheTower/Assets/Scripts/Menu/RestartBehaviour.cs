using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartBehaviour : MonoBehaviour {

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void LifeAnimation()
    {
        //Time.timeScale = 0;
        //anim.SetTrigger("Death");
        
    }

    public void ResetTimer()
    {
        Time.timeScale = 1;
    }
}
