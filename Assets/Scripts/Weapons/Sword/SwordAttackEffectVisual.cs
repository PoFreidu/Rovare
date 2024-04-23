using System.Collections;
using System.Collections.Generic;
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
        sword.OnSwordAttack += Sword_OnSwordAttack;
    }

    private void Sword_OnSwordAttack(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ATTACK_EFFECT);
    }
}
