using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PurpleElevatorBehaviour : MonoBehaviour
{
    private bool _allowReset = true;
    public UnityEvent arrivedAtNewFloor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_allowReset)
        {
            arrivedAtNewFloor.Invoke();
            ChangeAllowReset();
        }
    }

    public void ChangeAllowReset()
    {
        _allowReset = !_allowReset;
    }

}
