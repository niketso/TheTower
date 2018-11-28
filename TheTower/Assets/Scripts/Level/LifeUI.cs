using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour {
    private int maxHp = 3;
    public PlayerHP plyr;
    public Image[] healthImages;
    
    void Update()
    {
        for (int i = 0; i < maxHp; i++)
        {
            if ( plyr.PlayerChances <= i)
            {

                healthImages[i].enabled = false;
                
            }
            else
            {
                healthImages[i].enabled = true;
            }

        }
    } 
}