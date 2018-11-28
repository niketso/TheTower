using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.Events;


public class ElevatorBehaviour : MonoBehaviour {

    [SerializeField] private float time;
    [SerializeField] private GameObject nextElevator;
    [SerializeField] private GameObject prompt;
    private Animator anim;
    private AudioSource audSource;
    private GameObject player;
    private float timer;

    public UnityEvent newFloor;
    public UnityEvent elevatorActive;

    public float Timer
    {
        get
        {
            return timer;
        }
    }

    private void Awake()
    {
        timer = time;
        anim = GetComponent<Animator>();
        audSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (timer <= 0)
        {
            timer = 0;
        }
        else
            timer -= Time.deltaTime;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        prompt.SetActive(true);

        if (Input.GetKeyDown(KeyCode.C))
        {
            player = collision.gameObject;
            player.GetComponent<PlayerHP>().CanBeHit = false;
            anim.SetBool("Active", true);
            elevatorActive.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            player = collision.gameObject;
            player.GetComponent<PlayerHP>().CanBeHit = false;
            anim.SetBool("Active", true);
            elevatorActive.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        prompt.SetActive(false);
        anim.SetBool("Active", false);
        if (player)
            player.GetComponent<PlayerHP>().CanBeHit = true;
    }

    public void TransportPlayer()
    {
        if (player != null)
        {
            player.transform.SetPositionAndRotation(new Vector3(nextElevator.transform.position.x, nextElevator.transform.position.y - 0.5f, 0), Quaternion.identity);
            newFloor.Invoke();
            player.GetComponent<PlayerHP>().CanBeHit = true;
        }
        else
            Debug.LogError("There's no player to Transport!");
    }

    public void PlayElevatorSound()
    {
        audSource.Play();
    }
}
