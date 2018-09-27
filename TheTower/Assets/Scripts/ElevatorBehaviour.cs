using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorBehaviour : MonoBehaviour {

    [SerializeField] private float time;
    [SerializeField] private GameObject nextElevator;
    private float timer;

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            collision.transform.SetPositionAndRotation(new Vector3(nextElevator.transform.position.x, nextElevator.transform.position.y + 1.5f,0), Quaternion.identity);
        }
    }
}
