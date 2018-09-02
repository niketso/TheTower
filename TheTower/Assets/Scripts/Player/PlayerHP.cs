using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHP : MonoBehaviour {

    [SerializeField] private float playerChances;
    private float playerHP = 1;
    public UnityEvent plyDeath;

    private void Awake()
    {
        if(plyDeath == null)
            plyDeath = new UnityEvent();
    }

    private void Update()
    {
        if (playerHP <= 0) 
        {
            plyDeath.Invoke();
            playerHP = 1;
        }

        if (Input.GetKeyDown(KeyCode.Q))
            TakeEnemyDamage(1f);
    }

    public void TakeEnemyDamage(float damage) 
    {
        playerHP -= damage;
    }
}
