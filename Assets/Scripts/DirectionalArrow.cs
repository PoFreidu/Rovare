using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DirectionalArrow : MonoBehaviour
{
	private Transform player; // Объект игрока
	private Transform exitArea; // Объект выхода
	private RectTransform arrowPointer; // UI элемент стрелки
	public float thresholdDistance = 1.0f; // Дистанция, при которой стрелка исчезнет
	
	private void Awake()
	{
		InitializeReferences();
	}

	private void OnEnable()
	{
		// Подписываемся на событие перезагрузки сцены
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		// Отписываемся от события перезагрузки сцены
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		// Инициализируем ссылки заново при каждой загрузке сцены
		InitializeReferences();
	}
	
	 private void InitializeReferences()
	{
		// Поиск объекта персонажа по тегу "Player"
		GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
		if (playerObject != null)
		{
			player = playerObject.transform;
		}
		
		// Поиск объекта стрелки по тегу "ArrowPointer"
		GameObject arrowObject = GameObject.FindGameObjectWithTag("ArrowPointer");
		if (arrowObject != null)
		{
			arrowPointer = arrowObject.GetComponent<RectTransform>();
		}
		
		// Поиск объекта выхода по тегу "Exit"
		GameObject exitObject = GameObject.FindGameObjectWithTag("Exit");
		if (exitObject != null)
		{
			exitArea = exitObject.transform;
		}
	}
	
	private void Update()
	{
		// Расстояние от игрока до выхода
		float distance = Vector3.Distance(player.position, exitArea.position);

		// Если игрок достаточно близко к выходу, скрываем стрелку
		if (distance < thresholdDistance)
		{
			arrowPointer.gameObject.SetActive(false);
		}
		else
		{
			arrowPointer.gameObject.SetActive(true);

			// Направление от игрока к выходу
			Vector2 direction = (exitArea.position - player.position).normalized;

			// Угол для поворота стрелки
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
	
			// Поворачиваем стрелку в сторону выхода
			arrowPointer.rotation = Quaternion.Euler(0, 0, angle);
		}
	}
}
