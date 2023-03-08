using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuffAbility : Ability
{
    public int buffStrength;

    public override void Activate(GameObject parent)
    {
        CharacterController characterController = parent.GetComponent<CharacterController>();
        int newAttackDamage = characterController.getAttackDamage() + buffStrength;
        Debug.Log(newAttackDamage);
        characterController.setAttackDamage(newAttackDamage);
    }
}
