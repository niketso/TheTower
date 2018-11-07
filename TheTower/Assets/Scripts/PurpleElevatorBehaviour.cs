using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PurpleElevatorBehaviour : MonoBehaviour
{
    public UnityEvent arrivedAtNewFloor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        arrivedAtNewFloor.Invoke();
    }

}
