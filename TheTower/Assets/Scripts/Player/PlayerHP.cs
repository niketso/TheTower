using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour 
{
    public float playerChances;

    public Action OnPlayerDeath;
    public Action OnPlayerRespawned;

    [SerializeField] private Transform playerSpawnPoint;
    private bool invulnerable;
    private ShaderController shader;
    private Animator anim;

    public bool Invulnerable 
    { 
        get => invulnerable; 
        set 
        {
            Debug.Log($"Invulnerable{value}");
            invulnerable = value;
        }
    }

    private void Awake()
    {
        Invulnerable = false;
        shader = GetComponent<ShaderController>();
        anim = GetComponent<Animator>();

        OnPlayerDeath += KillPlayer;

        ElevatorBehaviour.OnElevatorStart += ActivateInvulnerability;
        ElevatorBehaviour.OnElevatorFinish += DeactivateInvulnerability;
    }

    private void Start()
    {
        shader.TriggerSpawn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            TakeDamage(EnemyType.MELEE);
    }

    public void TakeDamage(EnemyType type) 
    {
       if (Invulnerable) return;

        GameManager.instance.spawner.SpawnSpecialEnemy(transform.position);

        switch (type)
        {
            case EnemyType.MELEE:
                // Play death by melee damage
                AudioManager.instance.Play("PlayerDeathSword", false);
                break;
            case EnemyType.RANGED:
                // Play death by ranged damage
                AudioManager.instance.Play("PlayerDeathTurret", false);
                break;
        }

        if (OnPlayerDeath != null) 
        {
            OnPlayerDeath.Invoke();
            Debug.Log("OnPlayerDeath called");
        }
    }

    private void KillPlayer()
    {
        playerChances--;

        Invulnerable = true;
        anim.SetBool("die" , true);

        shader.TriggerDespawn( success => 
        {
            if (playerChances > 0)
                RespawnPlayer();
            else
                SceneManager.LoadScene("LoseScreen");
        });
    }

    private void RespawnPlayer()
    {
        anim.SetBool("die" , false);
        transform.position = playerSpawnPoint.position;
        GameManager.instance.ChangeFloor(0);

        shader.TriggerSpawn(success => 
        {
            Invulnerable = false;

            if (OnPlayerRespawned != null)
                OnPlayerRespawned.Invoke();
        });
    }

    public void ActivateInvulnerability() 
    {
        Invulnerable = true;
    }

    public void DeactivateInvulnerability() 
    {
        Invulnerable = false;
    }
}