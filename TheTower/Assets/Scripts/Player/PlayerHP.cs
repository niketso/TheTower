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
        if (playerChances != 0)
        {
            if (playerHP <= 0) 
            {
                plyDeath.Invoke();
                playerHP = 1;
                playerChances -= 1;
                PlayerReset();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
            TakeEnemyDamage(1f);

        if (playerChances <= 0)
            Destroy(gameObject);
    }

    public void TakeEnemyDamage(float damage) 
    {
        playerHP -= damage;
    }

    private void PlayerReset()
    {
        transform.position = new Vector2(0, -2.50f);
    }
}
