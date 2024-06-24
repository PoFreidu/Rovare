using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))]
public class AutoAssignCamera : MonoBehaviour
{
	private Canvas canvas;

	void Awake()
	{
		canvas = GetComponent<Canvas>();
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		AssignCamera();
	}

/* 	private void AssignCamera()
	{
		// Поиск UICamera и установка ее как worldCamera для Canvas
		Camera uiCamera = GameObject.FindWithTag("UICamera").GetComponent<Camera>();
		if (uiCamera != null)
		{
			canvas.worldCamera = uiCamera;
		}
		else
		{
			Debug.LogError("PersistentCanvasCamera: UICamera с тегом 'UICamera' не найдена.");
		}
	} */
	
	private void AssignCamera()
	{
    	if (SceneManager.GetActiveScene().name != "Menu") // Проверка, что это не главное меню
    	{
        	Camera uiCamera = GameObject.FindWithTag("UICamera")?.GetComponent<Camera>();
        	if (uiCamera != null)
        	{
            	canvas.worldCamera = uiCamera;
        	}
        	else
        	{
            	Debug.LogError("UICamera с тегом 'UICamera' не найдена.");
        	}
    	}
	}

	void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded; // Отписаться от события
	}
}
