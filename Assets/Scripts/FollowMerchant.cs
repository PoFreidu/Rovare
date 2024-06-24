using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FollowMerchant : MonoBehaviour
{
	public Transform merchantTransform; // Объект торговца
	public Vector3 offset; // Смещение текста относительно торговца
	public TMP_Text merchantText;
	public float interactionDistance = 2f;
	private bool isPlayerNear = false;
	public HeartInteraction heartInteraction; // Ссылка на HeartInteraction

	void Start()
	{
		merchantTransform = GameObject.Find("Merchant")?.transform;
		heartInteraction = FindFirstObjectByType<HeartInteraction>();

		if (merchantTransform == null)
		{
			Debug.LogError("Merchant Transform не найден.");
			return; // Выходим из Start, если не найден
		}

		if (heartInteraction == null)
		{
			Debug.LogError("Heart Interaction не найден.");
			return; // Выходим из Start, если не найден
		}
		
		/* merchantText = GetComponent<TMP_Text>(); */
		GameObject merchantPhrasesObj = GameObject.Find("MerchantPhrases");
		merchantText = merchantPhrasesObj.GetComponent<TMP_Text>();
		merchantText.text = ""; // Изначально текст не отображается
	}
	
	void Update()
	{
		if (merchantTransform == null) return; // Проверяем, не был ли объект уничтожен

		merchantText.transform.position = merchantTransform.position + offset;
		merchantText.transform.LookAt(merchantText.transform.position + Camera.main.transform.rotation * Vector3.forward,
			Camera.main.transform.rotation * Vector3.up);

		// Проверяем дистанцию до игрока
		if (Vector3.Distance(merchantTransform.position, Player.Instance.transform.position) <= interactionDistance)
		{
			if (!isPlayerNear)
			{
				merchantText.text = "Подходи и покупай путник.";
				isPlayerNear = true;
			}
		}
		else
		{
			if (isPlayerNear)
			{
				merchantText.text = "";
				isPlayerNear = false;
			}
		}

		// Обновляем текст в зависимости от взаимодействий с сердцем
		if (heartInteraction.interactionsLeft <= 0)
		{
			merchantText.text = "Увы. Ты всё скупил.";
		}
	}
}

	/* void Start()
	{
		GameObject merchantObject = GameObject.Find("Merchant");
		merchantTransform = merchantObject.transform;
		heartInteraction = FindFirstObjectByType<HeartInteraction>();
		merchantTransform = GameObject.Find("Merchant")?.transform;
		heartInteraction = FindFirstObjectByType<HeartInteraction>();

		if (merchantTransform == null)
		{
			Debug.LogError("Merchant Transform не найден.");
		}

		if (heartInteraction == null)
		{
	   		Debug.LogError("Heart Interaction не найден.");
		}
		
		merchantText = GetComponent<TMP_Text>();
		merchantText.text = ""; // Изначально текст не отображается
	}
	
	void Update()
	{
		transform.position = merchantTransform.position + offset;
		transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
			Camera.main.transform.rotation * Vector3.up);

		// Проверяем дистанцию до игрока
		if (Vector3.Distance(merchantTransform.position, Player.Instance.transform.position) <= interactionDistance)
		{
			if (!isPlayerNear)
			{
				merchantText.text = "Подходи и покупай путник.";
				isPlayerNear = true;
			}
		}
		else
		{
			if (isPlayerNear)
			{
				merchantText.text = "";
				isPlayerNear = false;
			}
		}

		// Обновляем текст в зависимости от взаимодействий с сердцем
		if (heartInteraction.interactionsLeft <= 0)
		{
			merchantText.text = "Увы. Ты всё скупил.";
		}
	}
} */

/* public class FollowMerchant : MonoBehaviour
{
	public Transform merchant; // Объект торговца
	public Vector3 offset; // Смещение текста относительно торговца
	private TMP_Text merchantText;
	public float interactionDistance = 2f;
	private bool isPlayerNear = false;

	void Start()
	{
		merchantText = GetComponent<TMP_Text>();
		merchantText.text = ""; // Изначально текст не отображается
	}
	
	void Update()
	{
		transform.position = merchant.position + offset;
		transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
			Camera.main.transform.rotation * Vector3.up);

		// Проверяем дистанцию до игрока
		if (Vector3.Distance(merchant.position, Player.Instance.transform.position) <= interactionDistance)
		{
			if (!isPlayerNear)
			{
				merchantText.text = "Подходи и бери путник.";
				isPlayerNear = true;
			}
		}
		else
		{
			if (isPlayerNear)
			{
				merchantText.text = "";
				isPlayerNear = false;
			}
		}
	}

/* 	void Update()
	{
		// Обновляем позицию текста, чтобы он следовал за торговцем
		transform.position = merchant.position + offset;

		// Опционально: Поворачиваем текст к камере
		transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
			Camera.main.transform.rotation * Vector3.up);
	} */
/* } */
