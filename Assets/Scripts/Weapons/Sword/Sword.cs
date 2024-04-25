using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
	[SerializeField] private int _damageAmount = 5;

	public event EventHandler OnSwordAttack;

	private PolygonCollider2D _polygonCollider2D;

	private void Awake()
	{
		_polygonCollider2D = GetComponent<PolygonCollider2D>();
	}

	private void Start()
	{
		AttackColliderTurnOff();
	}

	public void Attack()
	{
		if (!PauseManager.isMenuPaused && _polygonCollider2D != null)
		{
		AttackColliderTurnOffOn();
		
		OnSwordAttack?.Invoke(this, EventArgs.Empty);	
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{	
		if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity))
		{
			enemyEntity.TakeDamage(_damageAmount);
		}
	}

	public void AttackColliderTurnOff()
	{
		_polygonCollider2D.enabled = false;
	}

	private void AttackColliderTurnOn()
	{
		_polygonCollider2D.enabled = true;
	}

	private void AttackColliderTurnOffOn()
	{
		AttackColliderTurnOff();
		AttackColliderTurnOn();
	}
}
