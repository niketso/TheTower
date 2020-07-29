using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private GameObject OptionsMenuUI;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private PlayerMovement plyMov;
    [SerializeField] private PlayerAttack plyAttack;
    public static bool gameIsPaused = false;

    private void Awake()
    {
       
    }
    private void Start()
    {
        volumeSlider.value = AudioManager.instance.startingVolume;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        OptionsMenuUI.SetActive(false);

        Time.timeScale = 1f;
        gameIsPaused = false;
        plyAttack.IsPaused = gameIsPaused;
        plyMov.IsPaused = gameIsPaused;
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        plyAttack.IsPaused = gameIsPaused;
        plyMov.IsPaused = gameIsPaused;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Title");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OptionsMenu()
    {
        PauseMenuUI.SetActive(false);
        OptionsMenuUI.SetActive(true);

    }

    public void BacktoPause()
    {
        OptionsMenuUI.SetActive(false);
        PauseMenuUI.SetActive(true);
    }

    public void SetVolume(float sliderValue)
    {        
        AudioManager.instance.startingVolume = sliderValue;
        PlayerPrefs.SetFloat("volume", sliderValue);
        AudioManager.instance.UpdateVolume(sliderValue);

    }
}

