using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchController : MonoBehaviour {

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeToLevel2()
    {
        anim.Play("CameraSwitch2");
    }

    public void ChangeToLevel3()
    {
        anim.Play("CameraSwitch3");
    }

    public void ChangeToLevel4()
    {
        anim.Play("CameraSwitch4");
    }

    public void RestartLevels()
    {
        anim.Play("CameraSwitch");
    }
}
