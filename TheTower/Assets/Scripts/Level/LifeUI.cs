using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{

    private int maxHp;
    private int i;
    public PlayerHP health;
    public GameObject[] hearts;
    [SerializeField] private Sprite deathSprite;
    [SerializeField] private Sprite aliveSprite;

    public int currentChances;

    private void Awake()
    {
        if (!health)
            health = GameManager.instance.Player.GetComponent<PlayerHP>();

        currentChances = health.playerChances;
        health.OnPlayerDeath += PlayAnimation;
    }

    public void PlayAnimation()
    {
        currentChances -= 1;
        hearts[currentChances].GetComponent<Image>().sprite = deathSprite;
        hearts[currentChances].GetComponent<Image>().CrossFadeAlpha(0.0f , 0.5f , true);
        hearts[currentChances].SetActive(false);
    }

    public void AddLife()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (!hearts[i].activeInHierarchy)
            {
                hearts[i].GetComponent<Image>().sprite = aliveSprite;
                hearts[i].SetActive(true);

                return;
            }
        }
    }
}