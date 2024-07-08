using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatMove : MonoBehaviour
{
    private NavMeshAgent navAgent;

    private Transform player;

    public float chaseDistance = 10f;

    private Vector3 startPosition;

    public float stopDistance = 2f;

    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position;
    }

    void Update()
    {
        // 플레이어와의 거리를 계산
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // 플레이어가 추적 거리 안에 있을 경우
        if (distanceToPlayer <= chaseDistance)
        {
            // 플레이어를 추적
            ChasePlayer(distanceToPlayer);
        }
        else
        {
            // 정찰 지점으로 이동
            Patrol();
        }
    }

    // 플레이어를 추적하는 함수
    void ChasePlayer(float distanceToPlayer)
    {
        // 플레이어가 너무 가까이 있을 경우
        if (distanceToPlayer <= stopDistance)
        {
            // 멈춘다
            navAgent.isStopped = true;
        }
        else
        {
            // 플레이어를 따라간다
            navAgent.isStopped = false;
            navAgent.SetDestination(player.position);
        }
    }

    // 정찰하는 함수
    void Patrol()
    {
        // 현재 정찰 지점에 도달했는지 확인
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
        {
            // 다음 정찰 지점으로 이동
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            navAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }
}
    

