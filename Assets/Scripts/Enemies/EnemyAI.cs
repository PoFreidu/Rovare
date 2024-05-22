using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.Utils;

public class EnemyAI : MonoBehaviour
{
	[SerializeField] private State startingState;
	[SerializeField] private float roamingDistanceMax = 7f;
	[SerializeField] private float roamingDistanceMin = 3f;
	[SerializeField] private float roamingTimerMax = 2f;

	private Knockback knockback;

	private NavMeshAgent navMeshAgent;
	private State state;
	private float roamingTime;
	private Vector3 roamingPosition;
	private Vector3 startingPosition;

	private enum State
	{
		Roaming
	}

	private void Start()
	{
		startingPosition = transform.position;
	}

	private void Awake()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
		navMeshAgent.updateRotation = false;
		navMeshAgent.updateUpAxis = false;
		state = startingState;
		knockback = GetComponent<Knockback>();
	}

	private void Update()
	{
		switch (state)
		{
			default:
			case State.Roaming:
				roamingTime -= Time.deltaTime;
				if (roamingTime < 0)
				{
					Roaming();
					roamingTime = roamingTimerMax;
				}
				break;
		}
	}
	
	private void FixedUpdate()
	{
		if (knockback.GettingKnockedBack) 
		{
			return;
		}
	}

	private void Roaming()
	{
		startingPosition = transform.position;
		roamingPosition = GetRoamingPosition();
		ChangeFaceDirection(startingPosition, roamingPosition);
		navMeshAgent.SetDestination(roamingPosition);
	}

	private Vector3 GetRoamingPosition()
	{
		return startingPosition + Utils.GerRandomDirection() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
	}

	private void ChangeFaceDirection(Vector3 sourcePosition, Vector3 targetPosition)
	{
		if (sourcePosition.x > targetPosition.x)
		{
			transform.rotation = Quaternion.Euler(0, -180, 0);
			
		}
		else
		{
			transform.rotation = Quaternion.Euler(0, 0, 0);
		}
	}
}