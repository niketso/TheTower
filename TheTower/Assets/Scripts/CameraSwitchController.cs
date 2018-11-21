using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchController : MonoBehaviour
{

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

    public void ChangeToLevel5()
    {
        anim.Play("CameraSwitch5");
    }

    public void ChangeToLevel6()
    {
        anim.Play("CameraSwitch6");
    }

    public void ChangeToLevel7()
    {
        anim.Play("CameraSwitch7");
    }

    public void ChangeToLevel8()
    {
        anim.Play("CameraSwitch8");
    }

    public void RestartLevels()
    {
        anim.Play("CameraSwitch");
    }
}
