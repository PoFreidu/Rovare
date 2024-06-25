using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
	public GameObject pausePanel;
	public static bool isMenuPaused = false;
	readonly string[] objectsToDestroy = { "Managers", "Player", "UICanvas" };

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
