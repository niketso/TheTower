using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinState : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        GameObject plyr = GameObject.FindGameObjectWithTag("Player");
        if(plyr) 
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
