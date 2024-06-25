using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;

public class CameraController : Singleton<CameraController>
{
	private CinemachineCamera cinemachineVirtualCamera;
	
	protected override void Awake()
	{
		base.Awake();
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void Start() 
	{
		SetPlayerCameraFollow();
	}
	
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		SetPlayerCameraFollow();
	}
	
	public void SetPlayerCameraFollow() 
	{
		if (SceneManager.GetActiveScene().name != "Menu") // Проверка на существование Player
		{
			cinemachineVirtualCamera = FindFirstObjectByType<CinemachineCamera>();
			if (cinemachineVirtualCamera != null)
			{
				cinemachineVirtualCamera.Follow = Player.Instance.transform;
			}
			else
			{
				Debug.LogError("CinemachineCamera не найдена.");
			}
		}
	}
	
	void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
}
