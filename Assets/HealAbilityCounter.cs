using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealAbilityCounter : MonoBehaviour
{
    [SerializeField] private float beginCountAt;
    [SerializeField] private bool isTimerOn = false;
    [SerializeField] private HealAbility healAbility;

    public Text timerText;
    void Start()
    {
        isTimerOn = true;
        beginCountAt = healAbility.activeTime;
        HealAbility.OnHealAbilityCollected += UpdateTimer;

    }


    void UpdateTimer()
    {
        StartCoroutine(UpdateTheTimer());
        IEnumerator UpdateTheTimer()
        {
            while (isTimerOn)
            {
                if (beginCountAt > 0)
                {
                    beginCountAt -= 1;
                }
                else
                {
                    Debug.Log("Time is UP!!");
                    beginCountAt = 0;
                    isTimerOn = false;
                }
                timerText.text = "Heal Ability Countdown: " + beginCountAt.ToString() + " Seconds";
                yield return new WaitForSeconds(1.0f);
            }

        }
    }
}
