using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Utils;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistanceMax = 5f;
    [SerializeField] private float roamingDistanceMin = 2f;
    [SerializeField] private float roamingTimerMax = 2f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private MonoBehaviour enemyAttackType;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool stopMovingWhileAttacking = false;

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
        InitializeNavMeshAgent();
        state = startingState;
        startingPosition = transform.position;
        timeRoaming = roamingTimerMax;
        roamPosition = GetRoamingPosition();
    }

    private void InitializeNavMeshAgent()
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
    }

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
        timeRoaming -= Time.deltaTime;

        if (timeRoaming <= 0)
        {
            roamPosition = GetRoamingPosition();
            timeRoaming = roamingTimerMax;
        }

        if (IsPlayerInRange())
        {
            state = State.Attacking;
        }
        else
        {
            navMeshAgent.SetDestination(roamPosition);
        }
    }
    
    private void Attacking()
    {
        if (!IsPlayerInRange())
        {
            state = State.Roaming;
            navMeshAgent.isStopped = false; // Разрешаем врагу двигаться
            roamPosition = GetRoamingPosition(); // Получаем новую позицию для роуминга
            navMeshAgent.SetDestination(roamPosition); // Устанавливаем новую цель для перемещения
        }
        else
        {
            if (canAttack)
            {
                canAttack = false;
                (enemyAttackType as IEnemy).Attack();

                navMeshAgent.isStopped = stopMovingWhileAttacking;

                StartCoroutine(AttackCooldownRoutine());
            }
        }
    }

    private bool IsPlayerInRange()
    {
        return (transform.position - Player.Instance.transform.position).sqrMagnitude < attackRange * attackRange;
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + Utils.GetRandomDirection() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }
    
    private IEnumerator AttackCooldownRoutine() 
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void OnDestroy()
    {
        StopAllCoroutines(); // Отмена всех корутин при уничтожении объекта
    }
}
