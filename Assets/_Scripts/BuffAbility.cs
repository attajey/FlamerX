using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuffAbility : Ability
{
    public int buffStrength;
    public static event Action OnBuffAbilityCollected;

    public override void Activate(GameObject parent)
    {
        CharacterController characterController = parent.GetComponent<CharacterController>();
        int newAttackDamage = characterController.getAttackDamage() + buffStrength;
        Debug.Log("New Attack Damage: " + newAttackDamage);
        characterController.setAttackDamage(newAttackDamage);

        OnBuffAbilityCollected?.Invoke();
    }

    public override void BeginCooldown(GameObject parent) 
    {
        CharacterController characterController = parent.GetComponent<CharacterController>();
        int defaultAttackDamage = characterController.getAttackDamage() - buffStrength;
        Debug.Log("Default Attack Damage: " + defaultAttackDamage);
        characterController.setAttackDamage(defaultAttackDamage);
        OnBuffAbilityCollected?.Invoke();
    }

}
