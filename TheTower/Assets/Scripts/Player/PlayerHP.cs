using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour {

    [SerializeField] private float playerChances;
    [SerializeField] private Transform playerSpawnPoint;
   // [SerializeField] private AudioClip deathByMeleeSound;
    //[SerializeField] private AudioClip deathByShotSound;
    private string lastHitType;
   // private AudioSource audSource;
    private float playerHP = 1;
    private Animator anim;
    public UnityEvent plyDeath;
    private Transform playerDeathPos;

    private bool canBeHit = true;

    public float PlayerChances
    {
        get{ return playerChances; }
        set { playerChances = value; }
    }

    public Transform PlayerDeathPos
    {
        get { return playerDeathPos; }
    }

    public bool CanBeHit
    {
        get { return canBeHit; }
        set { canBeHit = value; }
    }

    private void Awake()
    {
        if(plyDeath == null)
            plyDeath = new UnityEvent();

        playerDeathPos = transform;
        anim = GetComponent<Animator>();
        //audSource = GetComponent<AudioSource>();
        //audSource.volume = PlayerPrefs.GetFloat("volume");
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (PlayerChances != 0)
        {
            if (playerHP <= 0) 
            {
                gameObject.layer = 10;
                playerHP = 1;
                playerChances -= 1;
                PlayerReset();
                gameObject.layer = 9;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
            TakeEnemyDamage(1f, "Melee");

        if(PlayerChances <= 0) {

            Destroy(gameObject);
            SceneManager.LoadScene("LoseScreen");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void TakeEnemyDamage(float damage, string type) 
    {
        if (canBeHit)
        {
            playerHP -= damage;
            lastHitType = type;
            canBeHit = false;
        }
    }

    private void PlayerReset() {

        if (PlayerDeathPos.position != transform.position)
            PlayerDeathPos.position = transform.position;

        if (lastHitType == "Melee")
        {
            // audSource.clip = deathByMeleeSound;
           
           // audSource.volume = PlayerPrefs.GetFloat("volume");
        }

        else if (lastHitType == "Ranged")
        {
            // audSource.clip = deathByShotSound;
            //audSource.volume = PlayerPrefs.GetFloat("volume");
           // FindObjectOfType<AudioManager>().Play("PlayerDeathTurret");
        }
        

        StartCoroutine(PlayerDeath());
    }

    private IEnumerator PlayerDeath()
    {
        anim.SetBool("die", true);
        // audSource.volume = PlayerPrefs.GetFloat("volume");
        //solo para probar
        // FindObjectOfType<AudioManager>().Play("PlayerDeathSword");
        AudioManager.instance.Play("PlayerDeathSword");
        //audSource.Play();
        // audSource.clip = deathByShotSound;
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1;
        plyDeath.Invoke();
        transform.position = playerSpawnPoint.position;
        anim.SetBool("die", false);
        canBeHit = true;
    }
    
}
