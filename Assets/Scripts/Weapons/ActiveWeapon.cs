using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
	[SerializeField] private Sword sword;

	protected override void Awake()
	{
		base.Awake();
	}

	public Sword GetActiveWeapon()
	{
		return sword;
	}
}