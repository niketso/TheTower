using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour {

    [SerializeField] private float playerChances;
    [SerializeField] private Transform playerSpawnPoint;
    private float playerHP = 1;
    public UnityEvent plyDeath;
   private Transform playerDeathPos;

    public float PlayerChances
    {
        get
        {
            return playerChances;
        }
    }

    public Transform PlayerDeathPos
    {
        get
        {
            return playerDeathPos;
        } 
    }

    private void Awake()
    {
        if(plyDeath == null)
            plyDeath = new UnityEvent();

        playerDeathPos = transform;
    }

    private void Update()
    {
        if (PlayerChances != 0)
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

        if(PlayerChances <= 0) {

            Destroy(gameObject);
            SceneManager.LoadScene("LoseScreen");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
           
    }

    public void TakeEnemyDamage(float damage) 
    {
        playerHP -= damage;
        Debug.Log("Player has been hit");
    }

    private void PlayerReset() {

        if (PlayerDeathPos.position != transform.position)
        {
            PlayerDeathPos.position = transform.position;
        }

        transform.position = playerSpawnPoint.position;
    }
    
}
