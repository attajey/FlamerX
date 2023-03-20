using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class HealAbility : Ability
{

    public static event Action OnHealAbilityCollected;

    public override void Activate(GameObject parent)
    {
        CharacterController characterController = parent.GetComponent<CharacterController>();
        characterController.hasHealAbility = true;
        OnHealAbilityCollected?.Invoke();
    }

    public override void BeginCooldown(GameObject parent)
    {
        CharacterController characterController = parent.GetComponent<CharacterController>();
        characterController.hasHealAbility = false;
        OnHealAbilityCollected?.Invoke();
    }

}
