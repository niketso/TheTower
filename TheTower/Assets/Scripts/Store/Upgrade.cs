using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public int value;

    private StoreFunctionality myStore;

    public StoreFunctionality MyStore 
    {
        get => myStore;
        set => myStore = value;
    }

    public virtual void TriggerEffect(GameObject player) { }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (Input.GetKeyDown(KeyCode.C)) 
        {
            TriggerEffect(collision.gameObject);
            MyStore.DeactivateUpgrades();
        }
    }
}
