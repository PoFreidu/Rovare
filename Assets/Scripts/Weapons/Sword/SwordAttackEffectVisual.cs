using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordAttackVisual : MonoBehaviour
{
	[SerializeField] private Sword sword;

	private Animator animator;
	private const string ATTACK_EFFECT = "AttackEffect";

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void Start()
	{
		if (!PauseManager.isMenuPaused)
		{
		sword.OnSwordAttack += Sword_OnSwordAttack;
		}
	}

	private void Sword_OnSwordAttack(object sender, System.EventArgs e)
	{
		if (!PauseManager.isMenuPaused) 
		{
		animator.SetTrigger(ATTACK_EFFECT);
		}
	}
}
