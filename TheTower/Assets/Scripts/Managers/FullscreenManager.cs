using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class FullscreenManager : MonoBehaviour
{
    public static FullscreenManager instance;
    public bool defaultFullscreen = false;


    public static FullscreenManager Instance
       
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<FullscreenManager>();
            }
            return instance;
        }

    }

    private void Awake()
    {
        instance = this;

        if (!PlayerPrefs.HasKey("fullscreen"))
        {
            PlayerPrefs.SetString("fullscreen", defaultFullscreen.ToString());
        }
        else
        {
            Screen.fullScreen =  defaultFullscreen;
            Screen.SetResolution(1024 , 768 , false);
        }
    }
}
