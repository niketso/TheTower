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
    private GameObject player;
    private float timer;

    public UnityEvent newFloor;

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        prompt.SetActive(true);

        if (Input.GetKeyDown(KeyCode.C))
        {
            player = collision.gameObject;
            anim.SetBool("Active", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        prompt.SetActive(false);
        anim.SetBool("Active", false);
    }

    public void TransportPlayer()
    {
        if (player != null)
        {
            player.transform.SetPositionAndRotation(new Vector3(nextElevator.transform.position.x, nextElevator.transform.position.y + 1.5f, 0), Quaternion.identity);
            newFloor.Invoke();
        }
        else
            Debug.LogError("There's no player to Transport!");
    }
}
