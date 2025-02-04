using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
	[SerializeField] private int _maxHealth;
	[SerializeField] private GameObject _deathVFXPrefab;
	private int _currentHealth;
	private Knockback knockback;
	private Flash flash;

	private void Awake()
	{
		knockback = GetComponent<Knockback>();
		flash = GetComponent<Flash>();
	}

	private void Start()
	{
		_currentHealth = _maxHealth;
	}
	
	public void TakeDamage(int damage)
	{
		_currentHealth -= damage;
		knockback.GetKnockedBack(Player.Instance.transform, 15f);
		StartCoroutine(flash.FlashRoutine());
		DetectDeath();
/* 		StartCoroutine(CheckDetectDeathRoutine()); */
	}
	
/* 	private IEnumerator CheckDetectDeathRoutine() 
	{
		yield return new WaitForSeconds(flash.GetRestoreMatTime());
		DetectDeath();
	} */

	public void DetectDeath()
	{
		if (_currentHealth <=0)
		{
			GameObject deathVFXInstance = Instantiate(_deathVFXPrefab, transform.position, Quaternion.identity);
			Destroy(deathVFXInstance, 1);
			GetComponent<PickUpSpawner>().DropItems();
			Destroy(gameObject);
		}
	}
}
