using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuffAbility : Ability
{
    public int buffStrength;
    public static event Action OnAbilityCollected;

    public override void Activate(GameObject parent)
    {
        CharacterController characterController = parent.GetComponent<CharacterController>();
        int newAttackDamage = characterController.getAttackDamage() + buffStrength;
        Debug.Log(newAttackDamage);
        characterController.setAttackDamage(newAttackDamage);

        OnAbilityCollected?.Invoke();
    }

    public override void BeginCooldown(GameObject parent) 
    {
        CharacterController characterController = parent.GetComponent<CharacterController>();
        int defaultAttackDamage = characterController.getAttackDamage() - buffStrength;
        Debug.Log(defaultAttackDamage);
        characterController.setAttackDamage(defaultAttackDamage);
    }
}
