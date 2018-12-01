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

    private void Awake()
    {
        maxHp = healthAnimators.Length;
        i = healthAnimators.Length - 1;
    }

    public void PlayAnimation()
    {
        healthAnimators[i].sprite = deathSprite;
        healthAnimators[i].CrossFadeAlpha(0.0f, 0.5f, true);
        i--;
    }
}