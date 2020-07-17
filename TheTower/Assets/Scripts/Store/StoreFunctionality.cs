using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreFunctionality : MonoBehaviour
{
    public Upgrade[] upgrades;

    private void Awake()
    {
        foreach (Upgrade upgrade in upgrades) 
        {
            upgrade.MyStore = this;
        }
    }

    public void DeactivateUpgrades() 
    {
        foreach (Upgrade upgrade in upgrades)
        {
            upgrade.gameObject.SetActive(false);
        }
    }
}
