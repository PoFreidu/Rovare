using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/* public class FollowMerchantTextPhrases : MonoBehaviour
{
	public TMP_Text merchantText;
	public Vector3 offset; // Смещение текста относительно торговца
	private Transform merchantTransform; // Для хранения ссылки на Transform торговца

	void Awake()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		var merchant = FindFirstObjectByType<Merchant>();
		if (merchant != null)
		{
			merchantTransform = merchant.transform;
			UpdatePosition(); // Обновляем позицию сразу после нахождения торговца
		}
		else
		{
			Debug.LogError("Merchant не найден.");
		}
	}

	void Start()
	{
		merchantText = GetComponent<TMP_Text>();
		if (merchantText == null)
		{
			Debug.LogError("TMP_Text компонент не найден на объекте.");
		}
		merchantText.text = ""; // Изначально текст не отображается
	}

	void Update()
	{
		if (merchantTransform != null)
		{
			UpdatePosition(); // Обновляем позицию текста каждый кадр
		}
	}

	public void ShowMerchantText(string text)
	{
		if (merchantText != null)
		{
			merchantText.text = text;
		}
	}

	public void HideMerchantText()
	{
		if (merchantText != null)
		{
			merchantText.text = "";
		}
	}

	void UpdatePosition()
	{
		if (merchantTransform != null)
		{
			transform.position = Camera.main.WorldToScreenPoint(merchantTransform.position + offset);
		}
	}

	void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
} */

/* public class FollowMerchantTextPhrases : MonoBehaviour
{
	public TMP_Text merchantText;
	public Vector3 offset; // Смещение текста относительно торговца

	void Awake()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		UpdateMerchantTransform();
	}

	void UpdateMerchantTransform()
	{
		var merchant = FindFirstObjectByType<Merchant>();
		if (merchant != null)
		{
			transform.position = merchant.transform.position + offset;
		}
		else
		{
			Debug.LogError("Merchant не найден.");
		}
	}

	void Start()
	{
		merchantText = GetComponent<TMP_Text>();
		merchantText.text = ""; // Изначально текст не отображается
	}

	void Update()
	{
		UpdatePosition(); // Обновляем позицию текста каждый кадр
	}

	public void ShowMerchantText(string text)
	{
		merchantText.text = text;
	}

	public void HideMerchantText()
	{
		merchantText.text = "";
	}

	void UpdatePosition()
	{
		Transform cameraTransform = Camera.main.transform;
		if (cameraTransform != null && merchantText != null)
		{
			transform.LookAt(transform.position + cameraTransform.rotation * Vector3.forward,
				cameraTransform.rotation * Vector3.up);
		}
	}

	void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
} */

/* public class FollowMerchantTextPhrases : MonoBehaviour
{
	public TMP_Text merchantText;
	public Vector3 offset; // Смещение текста относительно торговца
	private Transform merchantTransform; // Ссылка на Transform торговца

	void Start()
	{
		merchantText = GetComponent<TMP_Text>();
		merchantText.text = ""; // Изначально текст не отображается
		merchantTransform = FindFirstObjectByType<Merchant>().transform; // Поиск Transform компонента торговца
	}
	
	void Update()
	{
		UpdatePosition(); // Обновляем позицию текста каждый кадр
	}

	public void ShowMerchantText(string text)
	{
		merchantText.text = text;
	}

	public void HideMerchantText()
	{
		merchantText.text = "";
	}

	void UpdatePosition()
	{
		if (merchantTransform != null)
		{
			Transform cameraTransform = Camera.main.transform;
			transform.position = merchantTransform.position + offset;
			transform.LookAt(transform.position + cameraTransform.rotation * Vector3.forward,
				cameraTransform.rotation * Vector3.up);
		}
	}
} */

public class FollowMerchantTextPhrases : MonoBehaviour
{
	public TMP_Text merchantText;
	public Vector2 offset;
	public Merchant merchant;
	private RectTransform textRectTransform;

	void Awake()
	{
		InitializeComponents();
		FindMerchant();
	}

	void InitializeComponents()
	{
		merchantText = GetComponent<TMP_Text>();
		textRectTransform = merchantText.GetComponent<RectTransform>();
		// Удалено добавление CanvasGroup и управление его прозрачностью.
	}
	
	void FindMerchant()
	{
		merchant = FindFirstObjectByType<Merchant>();
		if (merchant == null)
		{
			Debug.LogError("Объект Merchant не найден.");
		}
	}

	void Start()
	{
		merchant = FindFirstObjectByType<Merchant>();
		if (merchant == null)
		{
			Debug.LogError("Объект Merchant не найден.");
			return;
		}
	}

	void LateUpdate()
	{
		if (merchant == null) return;

		UpdatePosition();
	}

	public void ShowMerchantText(string text)
	{
		if (merchant == null)
		{
			HideMerchantText();
			return;
		}

		merchantText.text = text;
		merchantText.enabled = true; // Включаем текст, вместо управления прозрачностью.
		UpdatePosition();
	}

	public void HideMerchantText()
	{
		merchantText.enabled = false; // Выключаем текст, вместо управления прозрачностью.
	}

	void UpdatePosition()
	{
		if (merchant == null || Camera.main == null) return;

		Vector2 screenPosition = Camera.main.WorldToScreenPoint(merchant.transform.position);
		Vector2 canvasPosition;
		RectTransform canvasRect = merchantText.canvas.GetComponent<RectTransform>();

		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPosition, Camera.main, out canvasPosition))
		{
			textRectTransform.localPosition = canvasPosition + offset;
		}
		else
		{
			Debug.LogError("Ошибка преобразования координат экрана в локальные координаты канваса.");
		}
	}
}
