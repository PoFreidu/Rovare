using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
	public GameObject pausePanel;
	public static bool isMenuPaused = false;
	
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
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		isMenuPaused = false;
	}
	
	public void Menu()
	{
		SceneManager.LoadScene("Menu");
		isMenuPaused = false;
	}	
	
	public void ExitGame()
	{
		Application.Quit();
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
/* 	void Start()
	{
		pausePanel.SetActive(false);
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{	
			if(Paused == true)
			{
				pausePanel.SetActive(true);
				Time.timeScale = 0f;
				Paused = false;
			}
			else
			{
				pausePanel.SetActive(false);
				Time.timeScale = 1f;
				Paused = true;
			}
/* 			EscKeyPressed();
			EscKeyUnPressed(); */
/* 		}
	}
	
	public void Resume()
	{
		Time.timeScale = 1f;
		pausePanel.SetActive(false);
	} */
	
/* 	public void EscKeyPressed()
	{

	} */
	
/* 	public void EscKeyUnPressed()
	{

	} */ 
}
