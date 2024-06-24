using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Merchant : MonoBehaviour
{
	public float interactionDistance = 2f;
	private bool isPlayerNear = false;
	public HeartInteraction heartInteraction; // Ссылка на HeartInteraction
	private FollowMerchantTextPhrases followMerchantTextPhrases; // Ссылка на MerchantPhrases

/*	void Awake()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		followMerchantTextPhrases = FindFirstObjectByType<FollowMerchantTextPhrases>();
		if (followMerchantTextPhrases == null)
		{
			Debug.LogError("FollowMerchantTextPhrases не найден.");
		}

		heartInteraction = FindFirstObjectByType<HeartInteraction>();
		if (heartInteraction == null)
		{
			Debug.LogError("Heart Interaction не найден.");
		}
	}

	void Start()
	{
		// Инициализация перенесена в OnSceneLoaded
	}

	void Update()
	{
		CheckPlayerDistance();
		UpdateMerchantText();
	}

	void CheckPlayerDistance()
	{
		// Проверяем дистанцию до игрока
		if (Vector3.Distance(transform.position, Player.Instance.transform.position) <= interactionDistance)
		{
			if (!isPlayerNear)
			{
				followMerchantTextPhrases?.ShowMerchantText("Подходи и покупай путник.");
				isPlayerNear = true;
			}
		}
		else if (isPlayerNear)
		{
			followMerchantTextPhrases?.HideMerchantText();
			isPlayerNear = false;
		}
	}

	void UpdateMerchantText()
	{
		// Обновляем текст в зависимости от взаимодействий с сердцем
		if (heartInteraction != null && heartInteraction.interactionsLeft <= 0)
		{
			followMerchantTextPhrases?.ShowMerchantText("Увы. Ты всё скупил.");
		}
	}

	void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
} */

/* 	void Awake()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		UpdateFollowMerchantTextPhrasesReference();
	}

	void UpdateFollowMerchantTextPhrasesReference()
	{
		followMerchantTextPhrases = FindFirstObjectByType<FollowMerchantTextPhrases>();
		if (followMerchantTextPhrases == null)
		{
			Debug.LogError("FollowMerchantTextPhrases не найден.");
		}
	}
	void Start()
	{
		heartInteraction = FindFirstObjectByType<HeartInteraction>();
		followMerchantTextPhrases = FindFirstObjectByType<FollowMerchantTextPhrases>();

		if (heartInteraction == null)
		{
			Debug.LogError("Heart Interaction не найден.");
			return; // Выходим из Start, если не найден
		}

		if (followMerchantTextPhrases == null)
		{
			Debug.LogError("MerchantPhrases не найден.");
			return; // Выходим из Start, если не найден
		}
	}

	void Update()
	{
		// Проверяем дистанцию до игрока
		if (Vector3.Distance(transform.position, Player.Instance.transform.position) <= interactionDistance)
		{
			if (!isPlayerNear)
			{
				followMerchantTextPhrases.ShowMerchantText("Подходи и покупай путник.");
				isPlayerNear = true;
			}
		}
		else
		{
			if (isPlayerNear)
			{
				followMerchantTextPhrases.HideMerchantText();
				isPlayerNear = false;
			}
		}

		// Обновляем текст в зависимости от взаимодействий с сердцем
		if (heartInteraction.interactionsLeft <= 0)
		{
			followMerchantTextPhrases.ShowMerchantText("Увы. Ты всё скупил.");
		}
	}
} */


	void Awake()
	{
		Debug.Log("Merchant Awake");
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		UpdateFollowMerchantTextPhrasesReference();
	}

	void UpdateFollowMerchantTextPhrasesReference()
	{
		followMerchantTextPhrases = FindFirstObjectByType<FollowMerchantTextPhrases>();
		if (followMerchantTextPhrases == null)
		{
			Debug.LogError("FollowMerchantTextPhrases не найден.");
		}
	}

	void Start()
	{
		Debug.Log("Merchant Start");
		heartInteraction = FindFirstObjectByType<HeartInteraction>();
		if (heartInteraction == null)
		{
			Debug.LogError("Heart Interaction не найден.");
			return;
		}
	}
	void OnEnable()
{
    Debug.Log("Merchant OnEnable");
    // Остальной код...
}

void OnDisable()
{
    Debug.Log("Merchant OnDisable");
    // Остальной код...
}

void OnDestroy()
{
    Debug.Log("Merchant OnDestroy");
    // Остальной код...
}

	void Update()
	{
		if (followMerchantTextPhrases == null) return;

		CheckPlayerDistance();
		UpdateMerchantText();
	}

	void CheckPlayerDistance()
	{
		if (followMerchantTextPhrases == null) return;

		if (Vector3.Distance(transform.position, Player.Instance.transform.position) <= interactionDistance)
		{
			if (!isPlayerNear)
			{
				followMerchantTextPhrases.ShowMerchantText("Подходи и покупай путник.");
				isPlayerNear = true;
			}
		}
		else if (isPlayerNear)
		{
			followMerchantTextPhrases.HideMerchantText();
			isPlayerNear = false;
		}
	}

	void UpdateMerchantText()
	{
		if (heartInteraction == null || followMerchantTextPhrases == null) return;

		if (heartInteraction.interactionsLeft <= 0)
		{
			followMerchantTextPhrases.ShowMerchantText("Увы. Ты всё скупил.");
		}
	}
}
