using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PurpleElevator : MonoBehaviour {

    public UnityEvent newFloor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        newFloor.Invoke();
    }
}
