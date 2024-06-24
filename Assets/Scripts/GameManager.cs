using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Player player; // Ссылка на скрипт персонажа
	public Slider healthSlider; // Ссылка на слайдер HP
	public TMP_Text coinAmountText; // Ссылка на текст количества монет

	private Vector3 initialPlayerPosition; // Начальная позиция персонажа
	private float initialHealth; // Начальное значение HP
	private int initialCoinCount; // Начальное количество монет

	void Start()
	{
		// Сохраняем начальные значения
		initialPlayerPosition = player.transform.position;
		initialHealth = healthSlider.value;
		initialCoinCount = int.Parse(coinAmountText.text);
	}

	// Метод для перезапуска игры
	public void RestartGame()
	{
		// Загрузка первой сцены по индексу (0)
		SceneManager.LoadScene(0);
		
		// Сброс всех необходимых данных и состояний
		ResetGameStates();
	}

	// Метод для сброса состояний игры
	public void ResetGameStates()
	{
		// Сброс позиции персонажа
		player.transform.position = initialPlayerPosition;
		
		// Сброс слайдера HP
		healthSlider.value = initialHealth;
		
		// Сброс текста количества монет
		coinAmountText.text = initialCoinCount.ToString();
	}
}
