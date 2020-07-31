using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    
    
    [SerializeField] private PlayerMovement playerMov;
    [SerializeField] private Slider dashSlider;
   
    private static UIManager instance = null;

    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }

    void Update ()
    {             
        dashSlider.value = dashSlider.maxValue - playerMov.TimerToNextDash;

        if (dashSlider.value != dashSlider.maxValue && !AudioManager.instance.SoundPlaying("dashBar"))  /* chequear que el audio no se este corriendo actualmente */
        {
            AudioManager.instance.Play("DashBar", false);
            
        }
	}
}
