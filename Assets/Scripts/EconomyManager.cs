using UnityEngine;
using TMPro;

public class EconomyManager : Singleton<EconomyManager>
{
    private TMP_Text coinText;
    public int currentCoin = 0;
    const string COIN_AMOUNT_TEXT = "CoinAmountText";

    protected override void Awake()
    {
        base.Awake();
        ResetCoins();
    }

    private void Start()
    {
        coinText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
    }

    public void UpdateCurrentGoldCoin()
    {
        currentCoin += 1;
        UpdateCoinText(); 
    }

    public void ResetCoins()
    {
        currentCoin = 0;
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        if (coinText == null)
        {
            coinText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }
        coinText.text = currentCoin.ToString("D3");
    }

    public void SpendCoins(int amount)
    {
        if (currentCoin >= amount)
        {
            currentCoin -= amount;
            UpdateCoinText();
        }
    }
}
