using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour {

    private int maxHp;
    private int i;
    public PlayerHP plyr;
    public Image[] healthAnimators;
    [SerializeField] private Sprite deathSprite;
    [SerializeField] private Sprite aliveSprite;

    private void Awake()
    {
        maxHp = healthAnimators.Length;
        i = healthAnimators.Length - 2;
    }

    public void PlayAnimation()
    {
        healthAnimators[i].sprite = deathSprite;
        healthAnimators[i].CrossFadeAlpha(0.0f, 0.5f, true);
        i--;
    }

    public void AddLife() 
    {
        i++;
        healthAnimators[i].sprite = aliveSprite;
        healthAnimators[i].CrossFadeAlpha(1.0f, 0.5f, true);
    }
}