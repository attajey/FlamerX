using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    static int coinCount = 0;
    private Text cointText;

    void Start()
    {
        cointText = GetComponent<Text>();
        cointText.text = $"Coins: {coinCount}";
    }
    private void OnEnable()
    {
        Coin.OnCoinCollected += IncreamentCoinCount;
    }

    private void OnDisable()
    {
        
        Coin.OnCoinCollected -= IncreamentCoinCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncreamentCoinCount()
    {
        coinCount++;
        GlobalVariables.itemsCollected++;
        cointText.text = $"Coins: {coinCount}";
        //cointText.SetText($"Coins: {coinCount}");
    }
}
