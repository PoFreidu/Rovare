using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
	[SerializeField] private int enemyCounter; // Счётчик врагов
/* 	public GameObject levelCompleteWindow; // Окно завершения уровня */
/* 	public TextMeshProUGUI timeText; // Текст для отображения времени */
/* 	private float startTime; // Время начала уровня
	private bool timerActive = true;
	public static bool isLevelCompleteWindow = false; */

	public GameObject objectToRemove; // Объект для удаления после уничтожения всех врагов

	void Start()
	{
		// Подсчитываем количество врагов на сцене по тегу
		enemyCounter = GameObject.FindGameObjectsWithTag("Enemy").Length;
/* 		startTime = Time.time; // Запоминаем время начала уровня */
/* 		levelCompleteWindow.SetActive(false); // Скрываем окно завершения уровня */
	}

	void Update()
	{
/* 		if(timerActive)
		{

		} */
			// Пересчитываем количество врагов на сцене
			enemyCounter = GameObject.FindGameObjectsWithTag("Enemy").Length;
			// Если врагов не осталось, удаляем объект
			if (enemyCounter == 0)
			{
/* 				ShowLevelCompleteWindow(); */
				Destroy(objectToRemove);
			}
	}
	
/* 	void ShowLevelCompleteWindow()
	{
		float elapsedTime = Time.time - startTime; // Рассчитываем прошедшее время
		timeText.text = FormatTime(elapsedTime); // Обновляем текстовое поле с временем
		levelCompleteWindow.SetActive(true); // Показываем окно
		isLevelCompleteWindow = true; // Устанавливаем переменную в true, когда окно активно
		UpdateTimeScale(); // Останавливаем время в игре
	} */
	
/* 	void UpdateTimeScale()
	{
		if (isLevelCompleteWindow)
		{
			Time.timeScale = 0f; // Останавливаем время в игре
			AudioListener.pause = true; // Останавливаем все звуки
		}
		else
		{
			Time.timeScale = 1f; // Возобновляем время в игре
			AudioListener.pause = false; // Возобновляем все звуки
		}
	}

	string FormatTime(float time)
	{
		int minutes = (int)(time / 60);
		int seconds = (int)(time % 60);
		int milliseconds = (int)((time * 1000) % 1000);
		return string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
	} */
}
