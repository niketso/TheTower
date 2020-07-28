using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PurpleElevatorBehaviour : MonoBehaviour
{
    //private bool _allowReset = true;
    //private Animator anim;
    //public UnityEvent arrivedAtNewFloor;
    //
    //private void Awake()
    //{
    //    anim = GetComponent<Animator>();
    //}
    //
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    anim.SetBool("Active", true);
    //    StartCoroutine(InvokeEvent());
    //}
    //
    //public void ChangeAllowReset()
    //{
    //    _allowReset = true;
    //}
    //
    //private IEnumerator InvokeEvent()
    //{
    //    yield return new WaitForSecondsRealtime(0.12f);
    //    anim.SetBool("Active", false);
    //    if (_allowReset)
    //    {
    //        arrivedAtNewFloor.Invoke();
    //        _allowReset = false;
    //    }
    //}
}
