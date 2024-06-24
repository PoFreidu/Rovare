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

/* 	public void SetPlayerCameraFollow() 
	{
		cinemachineVirtualCamera = FindFirstObjectByType<CinemachineCamera>();
		cinemachineVirtualCamera.Follow = Player.Instance.transform;
	} */

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

/* public class CameraManager : MonoBehaviour
{
	public static CameraManager Instance;

	public Camera UICamera;
	public Canvas UICanvas;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (UICanvas != null)
		{
			UICanvas.worldCamera = UICamera;
			Debug.Log("CameraManager: UICamera assigned to UICanvas");
		}
		else
		{
			Debug.LogError("CameraManager: UICanvas is null or UICamera is missing");
		}
	}
} */

/* public class CameraController : MonoBehaviour
{
	public Camera UICamera;
	public Canvas UICanvas;

	private void Awake()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (UICanvas != null)
		{
			UICanvas.worldCamera = UICamera;
			Debug.Log("OnSceneLoaded: UICamera assigned to UICanvas");
		}
		else
		{
			Debug.LogError("OnSceneLoaded: UICanvas is null");
		}
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
} */

/* public class CameraController : Singleton<CameraController>
{
	public Camera UICamera;
	public Canvas UICanvas;

	protected override void Awake() 
	{
		base.Awake();
		Debug.Log("CameraController Awake: Instance created");
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Debug.Log($"Scene Loaded: {scene.name}");
		StartCoroutine(AssignCameraToCanvasWithDelay());
	}

	private IEnumerator AssignCameraToCanvasWithDelay()
	{
		yield return new WaitForSeconds(30f); // Задержка в 0.1 секунды
		if (UICanvas != null && UICamera != null)
		{
			Debug.Log("Before AssignCameraToCanvas: UICanvas and UICamera are both not null");
			AssignCameraToCanvas();
		}
		else
		{
			Debug.LogError("AssignCameraToCanvasWithDelay: UICanvas or UICamera is null");
		}
	}

	private void AssignCameraToCanvas()
	{
		if (UICanvas != null && UICamera != null)
		{
			UICanvas.worldCamera = UICamera;
			Debug.Log("AssignCameraToCanvas: UICamera assigned to UICanvas");
		}
		else
		{
			Debug.LogError("AssignCameraToCanvas: Failed to assign UICamera to UICanvas");
		}
	}

	private void OnDestroy() 
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
		Debug.Log("CameraController OnDestroy: Instance destroyed");
	}
} */

/* public class CameraController : Singleton<CameraController>
{
	private CinemachineCamera cinemachineCamera;
	public Camera UICamera; // Публичная переменная для привязки UICamera
	public Canvas UICanvas; // Публичная переменная для привязки UICanvas

	protected override void Awake() 
	{
		base.Awake();
		Debug.Log("CameraController Awake: Instance created");
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void Start()
	{
		Debug.Log("CameraController Start: Restoring references and setting camera");
		RestoreReferences();
		SetPlayerCameraFollow();
		AssignCameraToCanvas();
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Debug.Log($"Scene Loaded: {scene.name}");
		RestoreReferences();
		SetPlayerCameraFollow();
		StartCoroutine(AssignCameraToCanvasWithDelay());
		AssignCameraToCanvas();
	}
	
	private IEnumerator AssignCameraToCanvasWithDelay()
	{
		yield return new WaitForSeconds(1f); // Задержка в 0.1 секунды
		AssignCameraToCanvas();
	}

	public void SetPlayerCameraFollow() 
	{
		cinemachineCamera = FindFirstObjectByType<CinemachineCamera>();
		if (cinemachineCamera != null && Player.Instance != null) {
			cinemachineCamera.Follow = Player.Instance.transform;
			Debug.Log("SetPlayerCameraFollow: Camera follow set to player");
		} else {
			Debug.LogError("SetPlayerCameraFollow: Failed to find CinemachineCamera or Player");
		}
	}

	private void AssignCameraToCanvas()
	{
		if (UICanvas != null && UICamera != null && UICanvas.renderMode == RenderMode.ScreenSpaceCamera)
		{
			UICanvas.worldCamera = UICamera;
			Debug.Log("AssignCameraToCanvas: UICamera assigned to UICanvas");
		} else {
			Debug.LogError("AssignCameraToCanvas: Failed to assign UICamera to UICanvas");
		}
	}

	private void RestoreReferences()
	{
		if (UICamera == null)
		{
			UICamera = FindFirstObjectByType<Camera>();
			Debug.Log(UICamera != null ? "RestoreReferences: UICamera found" : "RestoreReferences: UICamera not found");
		}

		if (UICanvas == null)
		{
			UICanvas = FindFirstObjectByType<Canvas>();
			Debug.Log(UICanvas != null ? "RestoreReferences: UICanvas found" : "RestoreReferences: UICanvas not found");
		}
	}

	private void OnDestroy() 
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
		Debug.Log("CameraController OnDestroy: Instance destroyed");
	}
} */

/* public class CameraController : Singleton<CameraController>
{	
	private CinemachineCamera cinemachineCamera;
	public Camera UICamera; // Публичная переменная для привязки UICamera
	public Canvas UICanvas; // Публичная переменная для привязки UICanvas

	
	protected override void Awake() 
	{
		base.Awake();
		// Подписываемся на событие загрузки сцены
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	
	private void Start()
	{
		RestoreReferences();
		SetPlayerCameraFollow();
		AssignCameraToCanvas();
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		RestoreReferences();
		SetPlayerCameraFollow();
		AssignCameraToCanvas();
	}
	
	public void SetPlayerCameraFollow() 
	{
		 // Поиск объекта CinemachineVirtualCamera в сцене
		cinemachineCamera = FindFirstObjectByType<CinemachineCamera>();
		if (cinemachineCamera != null && Player.Instance != null) {
			cinemachineCamera.Follow = Player.Instance.transform;
		}
	}
	
	private void AssignCameraToCanvas()
	{
		if (UICanvas != null && UICamera != null && UICanvas.renderMode == RenderMode.ScreenSpaceCamera)
		{
			UICanvas.worldCamera = UICamera; // Привязываем UICanvas к UICamera
		}
	}
	
	private void RestoreReferences()
	{
		if (UICamera == null)
		{
			UICamera = FindFirstObjectByType<Camera>(); // Ищем камеру в сцене
		}

		if (UICanvas == null)
		{
			UICanvas = FindFirstObjectByType<Canvas>(); // Ищем холст в сцене
		}
	}
	
	private void OnDestroy() 
	{
		// Отписываемся от события при уничтожении объекта
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
} */
