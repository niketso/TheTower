using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] private Text txt;
    [SerializeField] private ElevatorBehaviour elevator;
    [SerializeField] private Text lives;
    [SerializeField] private PlayerHP playerHP;
    [SerializeField] private Text dash;
    [SerializeField] private PlayerMovement playerMov;
    [SerializeField]
     private Slider dashSlider;
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
       
        //DontDestroyOnLoad(this.gameObject);
    }

    void Update ()
    {
        //txt.text = elevator.Timer.ToString();
        lives.text = playerHP.PlayerChances.ToString();
        dashSlider.value = dashSlider.maxValue - playerMov.TimerToNextDash;//dashSlider.maxValue - dashSlider.minValue;
        //dash.text = playerMov.TimeToDash.ToString();
	}
}
