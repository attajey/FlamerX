using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Coin : MonoBehaviour, ICollectible
{

    public static event Action OnCoinCollected;

    private void Start()
    {

    }
    public void Collect()
    {
        //OnCoinCollected += PlayRandomSFX;
        //PlayRandomSFX();
        //Debug.Log("Collected a coin!");
        GlobalVariables.itemsCollected++;
        GlobalVariables.totalScore += 100;
        Destroy(gameObject);
        OnCoinCollected?.Invoke();
    }




}
