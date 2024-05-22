using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
/* 	public bool isDead { get; private set; } */

	[SerializeField] private int maxHealth = 50;
	[SerializeField] private float knockBackThrustAmount = 10f;
	[SerializeField] private float damageRecoveryTime = 1f;
	
	private int currentHealth;
	private bool canTakeDamage = true;
	private Knockback knockback;
	private Flash flash;
	
	private void Awake() {
		flash = GetComponent<Flash>();
		knockback = GetComponent<Knockback>();
	}

	private void Start() {
/* 		isDead = false; */
		currentHealth = maxHealth;
	}
	
	private void OnCollisionStay2D(Collision2D other) {
	EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

	if (enemy && canTakeDamage) 
	{
		TakeDamage(5, other.transform);
		knockback.GetKnockedBack(other.gameObject.transform, knockBackThrustAmount);
		StartCoroutine(flash.FlashRoutine());
	}
	}
	
		public void TakeDamage(int damageAmount, Transform transform) {
		canTakeDamage = false;
		currentHealth -= damageAmount;
		StartCoroutine(DamageRecoveryRoutine());
	}
	
	private IEnumerator DamageRecoveryRoutine() 
	{
		yield return new WaitForSeconds(damageRecoveryTime);
		canTakeDamage = true;
	}
}
