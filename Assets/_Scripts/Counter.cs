using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] private float beginCountAt;
    [SerializeField] private bool isTimerOn = false;
    [SerializeField] private BuffAbility buffAbility;

    public Text timerText;
    void Start()
    {
        isTimerOn = true;
        beginCountAt = buffAbility.activeTime;
        BuffAbility.OnBuffAbilityCollected += UpdateTimer;

    }


    void UpdateTimer()
    {
        float tempCount = beginCountAt;
        StartCoroutine(UpdateTheTimer());
        IEnumerator UpdateTheTimer()
        {
            while (isTimerOn)
            {
                if (tempCount > 0)
                {
                    tempCount -= 1;
                }
                else
                {
                    Debug.Log("Time is UP!!");
                    tempCount = 0;
                    isTimerOn = false;
                }
                timerText.text = "Buff Ability Countdown: " + tempCount.ToString() + " Seconds";
                yield return new WaitForSeconds(1.0f);
            }

        }
    }
}
