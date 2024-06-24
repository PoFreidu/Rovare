using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Singleton<PlayerHealth>
{
/* 	public static PlayerHealth Instance { get; private set; } */
	
	public bool isDead { get; private set; }

	[SerializeField] private AudioSource damageTakeSound;
	[SerializeField] private int maxHealth = 3;
	[SerializeField] private float knockBackThrustAmount = 10f;
	[SerializeField] private float damageRecoveryTime = 1f;
	
	private Slider healthSlider;
	private int currentHealth;
	private bool canTakeDamage = true;
	private Knockback knockback;
	private Flash flash;
	const string HEALTH_SLIDER_TEXT = "HealthSlider";
	const string TOWN_TEXT = "SampleScene";
	readonly int DEATH_HASH = Animator.StringToHash("Death");
/* 	private GameManager gameManager; */
	
	protected override void Awake() {
		base.Awake();

/* 		Instance = this; */
		
		flash = GetComponent<Flash>();
		knockback = GetComponent<Knockback>();
	}

	private void Start()
	{
		isDead = false;
/* 		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); */
		
		currentHealth = maxHealth;
		UpdateHealthSlider();
	}
	
	private void OnCollisionStay2D(Collision2D other)
	{
	EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

	if (enemy && canTakeDamage) 
	{
		TakeDamage(5, other.transform);
		knockback.GetKnockedBack(other.gameObject.transform, knockBackThrustAmount);
		StartCoroutine(flash.FlashRoutine());
	}
	}
	
	public void ResetHealth()
	{
		currentHealth = maxHealth;
		UpdateHealthSlider();
	}
	
	public void HealPlayer() 
	{
		if (currentHealth < maxHealth)
		{
			currentHealth += 3;
			UpdateHealthSlider();
		}
	}
	
	public void TakeDamage(int damageAmount, Transform hitTransform)
	{
		if (!canTakeDamage || isDead)
		{
			return;
		}
		
		currentHealth -= damageAmount;
		UpdateHealthSlider();
		CheckIfPlayerDeath();
		
		if(!isDead)
		{
			knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
			StartCoroutine(flash.FlashRoutine());
			canTakeDamage = false;
			StartCoroutine(DamageRecoveryRoutine());
			damageTakeSound.Play();
		}
	}
	
	public void CheckIfPlayerDeath()
	{
		if (currentHealth <= 0 && !isDead)
		{
			isDead = true;
/* 			Destroy(ActiveWeapon.Instance.gameObject); */
			currentHealth = 0;
			Debug.Log("PlayerDeath");
			GetComponent<Animator>().SetTrigger(DEATH_HASH);
			
			GameObject activeWeapon = GameObject.Find("ActiveWeapon"); // Найти объект меча по имени
			
			if (activeWeapon != null)
			{
			Destroy(activeWeapon); // Удалить объект меча
			}
			
			foreach (Collider2D collider in GetComponents<Collider2D>())
			{
			collider.enabled = false;
			}
			
			StartCoroutine(DeathLoadSceneRoutine());
			
/* 			gameManager.ResetGameStates(); */
		}
	}
	
	private IEnumerator DeathLoadSceneRoutine()
	{
		yield return new WaitForSeconds(3f);
		Destroy(gameObject);
		ResetHealth();
		EconomyManager.Instance.ResetCoins();
		SceneManager.LoadScene(TOWN_TEXT);
	}
	
	private IEnumerator DamageRecoveryRoutine() 
	{
		yield return new WaitForSeconds(damageRecoveryTime);
		canTakeDamage = true;
	}
	
	private void UpdateHealthSlider() 
	{
		if (healthSlider == null)
		{
			healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
		}
		
		healthSlider.maxValue = maxHealth;
		healthSlider.value = currentHealth;
	}
}
