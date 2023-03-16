using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] private float beginCountAt;
    [SerializeField] private bool isTimerOn = false;
    [SerializeField] private GameObject player;

    public Text timerText;
    void Start()
    {
        isTimerOn = true;
        beginCountAt = player.GetComponent<AbilityHolder>().buffAbility.activeTime;
        BuffAbility.OnAbilityCollected += UpdateTimer;

    }

    void Update()
    {
        //if (isTimerOn)
        //{
        //    if (beginCountAt > 0)
        //    {
        //        beginCountAt -= Time.deltaTime;
        //        UpdateTimer(beginCountAt);
        //    }
        //    else
        //    {
        //        Debug.Log("Time is UP!!");
        //        beginCountAt = 0;
        //        isTimerOn = false;
        //    }
        //}

    }

    void UpdateTimer()
    {
        //currentTime += 1;
        //float mins = Mathf.FloorToInt(currentTime / 60);
        //float secs = Mathf.FloorToInt(currentTime % 60);
        StartCoroutine(UpdateTheTimer());
        IEnumerator UpdateTheTimer()
        {
            while (isTimerOn)
            {
                if (isTimerOn)
                {
                    if (beginCountAt > 0)
                    {
                        beginCountAt -= Time.deltaTime;
                        //UpdateTimer(beginCountAt);
                    }
                    else
                    {
                        Debug.Log("Time is UP!!");
                        beginCountAt = 0;
                        isTimerOn = false;
                    }
                }
                timerText.text = "Ability Countdown: " + beginCountAt.ToString("N0");

                yield return new WaitForSeconds(1.0f);
            }

        }
        //if (isTimerOn)
        //{
        //    if (beginCountAt > 0)
        //    {
        //        beginCountAt -= Time.deltaTime;
        //        //UpdateTimer(beginCountAt);
        //    }
        //    else
        //    {
        //        Debug.Log("Time is UP!!");
        //        beginCountAt = 0;
        //        isTimerOn = false;
        //    }
        //}
    }
}
