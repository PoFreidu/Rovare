using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class HeartInteraction : MonoBehaviour
{
	public EconomyManager economyManager; // Ссылка на EconomyManager
	public PlayerHealth playerHealth; // Ссылка на PlayerHealth
	public int costPerHeal = 5; // Стоимость восстановления
	public int maxInteractions = 5; // Максимальное количество взаимодействий
	public int interactionsLeft; // Оставшиеся взаимодействия
	private PlayerInputActions playerInputActions;

	private void Awake()
	{
		playerHealth = FindFirstObjectByType<PlayerHealth>();
		economyManager = FindFirstObjectByType<EconomyManager>();
		playerInputActions = new PlayerInputActions();
		playerInputActions.Player.Interaction.performed += _ => OnInteract();
		playerInputActions.Player.Interaction.Enable();
	}
	
	void Start()
	{
		interactionsLeft = maxInteractions;
	}
	
	void OnInteract()
	{
		if (Input.GetMouseButton(1)) // Проверка нажатия правой кнопки мыши
		{
			if (economyManager.currentCoin >= costPerHeal && interactionsLeft > 0)
			{
				economyManager.SpendCoins(costPerHeal); // Тратим монеты
				playerHealth.HealPlayer(); // Восстанавливаем хп персонажа
				interactionsLeft--; // Уменьшаем количество оставшихся взаимодействий
				if (interactionsLeft <= 0)
				{
					gameObject.SetActive(false); // Деактивируем объект сердца
				}
			}
		}
	}
	
	private void OnDestroy()
	{
		playerInputActions.Player.Interaction.Disable();
	}
}

/* public class HeartInteraction : MonoBehaviour
{
	public EconomyManager economyManager; // Ссылка на EconomyManager
	public PlayerHealth playerHealth; // Ссылка на PlayerHealth
	public TMP_Text merchantText; // Текст над торговцем
	public int costPerHeal = 5; // Стоимость восстановления
	public int maxInteractions = 5; // Максимальное количество взаимодействий
	private int interactionsLeft; // Оставшиеся взаимодействия

	void Start()
	{
		interactionsLeft = maxInteractions;
		/* merchantText.text = "Подходи и бери путник."; */
/*     }

	void OnMouseDown()
	{
		if (Input.GetMouseButton(1)) // Проверка нажатия правой кнопки мыши
		{
			if (economyManager.currentCoin >= costPerHeal && interactionsLeft > 0)
			{
				economyManager.SpendCoins(costPerHeal); // Тратим монеты
				playerHealth.HealPlayer(); // Восстанавливаем хп персонажа
				interactionsLeft--; // Уменьшаем количество оставшихся взаимодействий
				if (interactionsLeft <= 0)
				{
					merchantText.text = "Увы. Ты всё скупил."; // Меняем текст
					gameObject.SetActive(false); // Деактивируем объект сердца
				}
			}
		}
	}
} */
