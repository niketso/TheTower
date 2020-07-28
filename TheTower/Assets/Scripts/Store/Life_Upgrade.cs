using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life_Upgrade : Upgrade
{
    public LifeUI ui;

    public override void TriggerEffect(GameObject player)
    {
        PlayerHP hp = player.GetComponent<PlayerHP>();

        if (!hp) return;

        hp.playerChances += value;
        ui.AddLife();

        Debug.LogError($" {player.name}'s chances increased by {value} ");
    }
}
