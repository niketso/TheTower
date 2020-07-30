using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public int value;
    public GameObject prompt;

    private StoreFunctionality myStore;
    private GameObject target;

    public StoreFunctionality MyStore 
    {
        get => myStore;
        set => myStore = value;
    }

    private void Awake()
    {
        target = null;
    }

    public virtual void TriggerEffect(GameObject player) 
    {
        MyStore.DeactivateUpgrades();
    }

    private void Update()
    {
        if (!target) return;

        if (Input.GetKeyDown(KeyCode.C))
            TriggerEffect(target);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target) return;

        prompt.SetActive(true);

        if (collision.CompareTag("Player"))
            target = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (target && collision.gameObject == target) 
        {
            target = null;
            prompt.SetActive(false);
        }
    }
}