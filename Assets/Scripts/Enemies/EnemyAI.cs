using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.Utils;

public class EnemyAI : MonoBehaviour
{
	[SerializeField] private State startingState;
	[SerializeField] private float roamingDistanceMax = 5f;
	[SerializeField] private float roamingDistanceMin = 2f;
	[SerializeField] private float roamingTimerMax = 2f;
	[SerializeField] private float attackRange = 0f;
	[SerializeField] private MonoBehaviour enemyType;
	[SerializeField] private float attackCooldown = 2f;
	[SerializeField] private bool stopMovingWhileAttacking = false;

	private Knockback knockback;
	private NavMeshAgent navMeshAgent;
	private State state;
	private float timeRoaming;
	private Vector3 roamPosition;
	private Vector3 startingPosition;
	private bool canAttack = true;

	private enum State
	{
		Roaming, 
		Attacking
	}

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent не найден на объекте: " + gameObject.name);
            return;
        }

        if (!navMeshAgent.isOnNavMesh)
        {
            Debug.LogError("NavMeshAgent не находится на NavMesh: " + gameObject.name);
            return;
        }

        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        state = startingState;
        startingPosition = transform.position;
        timeRoaming = roamingTimerMax;
        roamPosition = GetRoamingPosition();
    }

/* 	private void Update()
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
	} */
	
	private void Update() 
	{
		MovementStateControl();
	}

	private void MovementStateControl() 
	{
		switch (state)
		{
			default:
			case State.Roaming:
				Roaming();
				break;

			case State.Attacking:
				Attacking();
				break;
		}
	}
	
    private void Roaming()
    {
        timeRoaming += Time.deltaTime;

        if (Vector3.Distance(transform.position, Player.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }

        if (timeRoaming > roamingTimerMax)
        {
            roamPosition = GetRoamingPosition();
            timeRoaming = 0;
        }

        navMeshAgent.SetDestination(roamPosition);
    }
	
    private void Attacking()
    {
        if (Vector3.Distance(transform.position, Player.Instance.transform.position) > attackRange)
        {
            state = State.Roaming;
        }

        if (attackRange != 0 && canAttack)
        {
            canAttack = false;
            (enemyType as IEnemy).Attack();

            if (stopMovingWhileAttacking)
            {
                navMeshAgent.isStopped = true;
            }
            else
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(roamPosition);
            }

            StartCoroutine(AttackCooldownRoutine());
        }
    }

	private void FixedUpdate()
	{
		if (knockback != null && knockback.GettingKnockedBack) 
		{
			return;
		}
	}

/* 	private void Roaming()
	{
		startingPosition = transform.position;
		roamingPosition = GetRoamingPosition();
		if (CanReachDestination(roamingPosition))
		{
			ChangeFaceDirection(startingPosition, roamingPosition);
			navMeshAgent.SetDestination(roamingPosition);
		}
	} */

	private bool CanReachDestination(Vector3 destination)
	{
		if (!navMeshAgent.isActiveAndEnabled || !navMeshAgent.isOnNavMesh)
		{
			return false;
		}

		NavMeshPath path = new NavMeshPath();
		if (navMeshAgent.CalculatePath(destination, path))
		{
			return path.status == NavMeshPathStatus.PathComplete;
		}
		return false;
	}

	private Vector3 GetRoamingPosition()
	{
		return startingPosition + Utils.GetRandomDirection() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
	}
	
/* 	    private Vector2 GetRoamingPosition() {
		timeRoaming = 0f;
		return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
	} */
	
	private IEnumerator AttackCooldownRoutine() 
	{
		yield return new WaitForSeconds(attackCooldown);
		canAttack = true;
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