﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private PlayerMovement plyMov;
    [SerializeField] private PlayerAttack plyAttack;
    public static bool gameIsPaused = false;

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
}

