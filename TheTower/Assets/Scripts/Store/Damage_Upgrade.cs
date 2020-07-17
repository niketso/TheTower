using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Upgrade : Upgrade
{
    public override void TriggerEffect(GameObject player)
    {
        PlayerAttack attack = player.GetComponent<PlayerAttack>();

        if (!attack) return;

        attack.Damage += value;

        Debug.LogError($" {player.name}'s attack is augmented by {value} ");
    }
}
