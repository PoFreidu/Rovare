using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordVisual : MonoBehaviour
{
	[SerializeField] private Sword sword;

	private Animator animator;
	private const string ATTACK = "Attack";

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
		animator.SetTrigger(ATTACK);	
		}
	}

	public void EndTriggerAttackEffectAnimation()
	{
		if (!PauseManager.isMenuPaused) 
		{
		sword.AttackColliderTurnOff();	
		}
	}
}
