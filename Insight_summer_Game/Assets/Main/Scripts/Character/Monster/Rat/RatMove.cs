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
        // �÷��̾���� �Ÿ��� ���
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // �÷��̾ ���� �Ÿ� �ȿ� ���� ���
        if (distanceToPlayer <= chaseDistance)
        {
            // �÷��̾ ����
            ChasePlayer(distanceToPlayer);
        }
        else
        {
            // ���� �������� �̵�
            Patrol();
        }
    }

    // �÷��̾ �����ϴ� �Լ�
    void ChasePlayer(float distanceToPlayer)
    {
        // �÷��̾ �ʹ� ������ ���� ���
        if (distanceToPlayer <= stopDistance)
        {
            // �����
            navAgent.isStopped = true;
        }
        else
        {
            // �÷��̾ ���󰣴�
            navAgent.isStopped = false;
            navAgent.SetDestination(player.position);
        }
    }

    // �����ϴ� �Լ�
    void Patrol()
    {
        // ���� ���� ������ �����ߴ��� Ȯ��
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
        {
            // ���� ���� �������� �̵�
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            navAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }
}
    

