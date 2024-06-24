using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EconomyManager : Singleton<EconomyManager>
{
	private TMP_Text CoinText;
	public int currentCoin = 0;
	const string COIN_AMOUNT_TEXT = "CoinAmountText";
	
	protected override void Awake()
	{
		base.Awake();
		ResetCoins();
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
		if (CoinText == null)
		{
			CoinText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
		}
		CoinText.text = currentCoin.ToString("D3");
	}
	
	public void SpendCoins(int amount)
	{
		if (currentCoin >= amount)
		{
			currentCoin -= amount;
			UpdateCoinText();
		}
	}
/* 	public void UpdateCurrentGoldCoin() {
		currentCoin += 1;

		if (CoinText == null) {
			CoinText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
		}

		CoinText.text = currentCoin.ToString("D3");
	} */
}
