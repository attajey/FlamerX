using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    public Ability buffAbility;
    public Ability healAbility;
    float buffCooldownTime;
    float buffActiveTime;

    float healCooldownTime;
    float healActiveTime;



    enum BuffAbilityState
    {
        ready,
        active,
        cooldown
    }

    enum HealAbilityState
    {
        ready,
        active,
        cooldown
    }

    BuffAbilityState buffState = BuffAbilityState.ready;
    HealAbilityState healState = HealAbilityState.ready;


    //public KeyCode key;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Buff Ability"))
        {
            if (buffState == BuffAbilityState.ready)
            {
                buffAbility.Activate(gameObject);
                buffState = BuffAbilityState.active;
                buffActiveTime = buffAbility.activeTime;

                Destroy(collision.gameObject);
            }
        }
        if (collision.CompareTag("Heal Ability"))
        {
            if (healState == HealAbilityState.ready)
            {
                healAbility.Activate(gameObject);
                healState = HealAbilityState.active;
                healActiveTime = healAbility.activeTime;
                Destroy(collision.gameObject);
            }
        }
    }


    void Update()
    {
        switch (buffState)
        {
            case BuffAbilityState.ready:
                //if (Input.GetKeyDown(key))
                //{
                //    buffAbility.Activate(gameObject);
                //    state = AbilityState.active;
                //    activeTime = buffAbility.activeTime;
                //}
                break;
            case BuffAbilityState.active:
                if (buffActiveTime > 0)
                {
                    buffActiveTime -= Time.deltaTime;
                }
                else
                {
                    buffAbility.BeginCooldown(gameObject);
                    buffState = BuffAbilityState.cooldown;
                    buffCooldownTime = buffAbility.cooldownTime;
                }
                break;
            case BuffAbilityState.cooldown:
                if (buffCooldownTime > 0)
                {
                    buffCooldownTime -= Time.deltaTime;
                }
                else
                {
                    buffState = BuffAbilityState.ready;
                }
                break;

            default:
                Debug.LogError("Ability State Not Found!");
                break;
        }


        switch (healState)
        {
            case HealAbilityState.ready:
                //if (Input.GetKeyDown(key))
                //{
                //    buffAbility.Activate(gameObject);
                //    state = AbilityState.active;
                //    activeTime = buffAbility.activeTime;
                //}
                break;
            case HealAbilityState.active:
                if (healActiveTime > 0)
                {
                    healActiveTime -= Time.deltaTime;
                }
                else
                {
                    healAbility.BeginCooldown(gameObject);

                    healState = HealAbilityState.cooldown;
                    healCooldownTime = healAbility.cooldownTime;
                }
                break;
            case HealAbilityState.cooldown:
                if (healCooldownTime > 0)
                {
                    healCooldownTime -= Time.deltaTime;
                }
                else
                {
                    healState = HealAbilityState.ready;
                }
                break;

            default:
                Debug.LogError("Ability State Not Found!");
                break;
        }


    }

}
