using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    private static CoinManager instance;
    private TextMeshProUGUI coinText;
    private float coins;
    void Start()
    {
        if (instance == null) instance = this;
        instance.coinText = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    // just add a negative number if you want to remove coins
    public static void AddCoins(float coinsToAdd)
    {
        instance.coins += coinsToAdd;
        instance.coinText.text = $": {instance.coins}";
    }
    public static float GetCoins() { return instance.coins; }
}
