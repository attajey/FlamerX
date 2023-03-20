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
    void Update()
    {
        //timerText.text = beginCountAt.ToString();
    }

    void UpdateTimer()
    {
        while (isTimerOn)
        {

            if (beginCountAt > 0)
            {
                beginCountAt -= Time.deltaTime;
                //UpdateTimer(beginCountAt);
                timerText.text = beginCountAt.ToString();
            }
            else
            {
                Debug.Log("Time is UP!!");
                beginCountAt = 0;
                isTimerOn = false;
            }
        }
        timerText.text = "Ability Countdown: " + beginCountAt.ToString("N0");
    }
}
