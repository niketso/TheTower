using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour {

    private float playerHP = 1;
    [SerializeField]
    private float playerChances;

    private void Update()
    {
        if (playerHP <= 0) 
        {
            //crear un enemigo en esta  posicion
        }
    }
    private void TakeEnemyDamage(float damage) 
    {
        playerHP -= damage;
    }
}
