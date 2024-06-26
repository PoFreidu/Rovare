using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
	public static PauseManager Instance { get; private set; }
	public GameObject pausePanel;
	public static bool isMenuPaused = false;
	readonly string[] objectsToDestroy = { "Managers", "Player", "UICanvas" };

	private void Awake()
	{
		Instance = this;
	}
	
	private void Start()
	{
		pausePanel.SetActive(false);
	}
	
	public void DestroyObjects(string[] objectsToDestroy)
	{
	foreach (var objectName in objectsToDestroy)
		{
			GameObject obj = GameObject.Find(objectName);
			if (obj != null)
			{
				Destroy(obj);
			}
		}
	}
	
/* 	public void TogglePause()
	{
		isMenuPaused = !isMenuPaused;
		pausePanel.SetActive(isMenuPaused);
		Time.timeScale = isMenuPaused ? 0f : 1f;
		AudioListener.pause = isMenuPaused;
	} */

	void Update()
	{
		PauseMenu();
	}
	
	void PauseMenu()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			isMenuPaused = !isMenuPaused;
		}
		
		if (isMenuPaused)
		{
			pausePanel.SetActive(true);
			
			Time.timeScale = 0f;
			AudioListener.pause = true;
		}
		
		else
		{
			pausePanel.SetActive(false);
			
			Time.timeScale = 1f;
			AudioListener.pause = false;
		}
	}
	
	public void Resume()
	{
		isMenuPaused = false;
	}	
	
	public void Restart()
	{
		isMenuPaused = false;
		PlayerHealth.Instance.ResetHealth();
		EconomyManager.Instance.ResetCoins();
		DestroyObjects(objectsToDestroy);
		SceneManager.LoadScene("SampleScene");
	}
	
	public void Menu()
	{
		isMenuPaused = false;
		SceneManager.LoadScene("Menu");
		
	}	
	
	public void ExitGame()
	{
		Application.Quit();
	}
}


/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
	public GameObject pausePanel;
	public static bool isMenuPaused = false; // Изменим на private

	private void Start()
	{
		pausePanel.SetActive(false);
	}
	
	void Update()
	{
		PauseMenu();
	}
	
	void PauseMenu()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !pausePanel.activeSelf)
		{
			TogglePauseMenu();
		}
	}
	
	public void TogglePauseMenu()
	{
		isMenuPaused = !isMenuPaused;
		pausePanel.SetActive(isMenuPaused);
		UpdateTimeScale(isMenuPaused);
	}
	
	void UpdateTimeScale(bool isPaused)
	{
		Time.timeScale = isPaused ? 0f : 1f;
		AudioListener.pause = isPaused;
	}
	
	public void Resume()
	{
		if (isMenuPaused)
		{
			TogglePauseMenu();
		}
	}
	
	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	
	public void Menu()
	{
		SceneManager.LoadScene("Menu");
	}    
	
	public void ExitGame()
	{
		Application.Quit();
	}
} */


// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class PauseManager : MonoBehaviour
// {
// 	public GameObject pausePanel;
// 	public GameObject levelCompleteWindow;
// 	public static bool isMenuPaused = false;
// 	readonly string[] objectsToDestroy = { "Managers", "Player", "UICanvas" };

// 	private void Start()
// 	{
// 		pausePanel.SetActive(false);
// 	}
	
// 	public void DestroyObjects(string[] objectsToDestroy)
// 	{
// 	foreach (var objectName in objectsToDestroy)
// 		{
// 			GameObject obj = GameObject.Find(objectName);
// 			if (obj != null)
// 			{
// 				Destroy(obj);
// 			}
// 		}
// 	}
	
// 	void Update()
// 	{
// 		PauseMenu();
// 	}
	
// 	void PauseMenu()
// 	{
// 		if (Input.GetKeyDown(KeyCode.Escape) && !EnemyCounter.isLevelCompleteWindow)
// 		{
// 			isMenuPaused = !isMenuPaused;
// 			pausePanel.SetActive(isMenuPaused);
// 			UpdateTimeScale();
// 		}
		
// /* 		if (isMenuPaused)
// 		{
// 			pausePanel.SetActive(true);
			
// 			Time.timeScale = 0f;
// 			AudioListener.pause = true;
// 		}
		
// 		else
// 		{
// 			pausePanel.SetActive(false);
			
// 			Time.timeScale = 1f;
// 			AudioListener.pause = false;
// 		} */
// 	}
	
// 	void UpdateTimeScale()
// 	{
// 		if (isMenuPaused || EnemyCounter.isLevelCompleteWindow)
// 		{
// 			Time.timeScale = 0f;
// 			AudioListener.pause = true;
// 		}
// 		else
// 		{
// 			Time.timeScale = 1f;
// 			AudioListener.pause = false;
// 		}
// 	}
	
// 	public void Resume()
// 	{
//     	// Проверяем, активно ли окно завершения уровня
//     	if (EnemyCounter.isLevelCompleteWindow)
//     	{
//         	// Если да, то выключаем его
//         	EnemyCounter.isLevelCompleteWindow = false;
//         	if (levelCompleteWindow != null)
//         	{
//             	levelCompleteWindow.SetActive(false);
//         	}
//     	}
//     	// Иначе, если окно завершения уровня не активно, но игра на паузе
//     	else if (isMenuPaused)
//     	{
//         	// То выключаем меню паузы
//         	isMenuPaused = false;
//         	if (pausePanel != null)
//         	{
//             	pausePanel.SetActive(false);
//         	}
//     	}
//     	UpdateTimeScale(); // Возобновляем время в игре и звуки
// 	}

	
// /* 	public void Resume()
// 	{
// 		isMenuPaused = false;
// 		EnemyCounter.isLevelCompleteWindow = false;
// 	} */	
	
// 	public void Restart()
// 	{
// 		isMenuPaused = false;
// 		PlayerHealth.Instance.ResetHealth();
// 		EconomyManager.Instance.ResetCoins();
// 		DestroyObjects(objectsToDestroy);
// 		SceneManager.LoadScene("SampleScene");
// 	}
	
// 	public void Menu()
// 	{
// 		isMenuPaused = false;
// 		SceneManager.LoadScene("Menu");
		
// 	}	
	
// 	public void ExitGame()
// 	{
// 		Application.Quit();
// 	}
// }
