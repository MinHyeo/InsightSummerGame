using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chacer : MonoBehaviour, IChacer
{

    public Transform targetPlayer; // �̵��� �÷��̾��� ��ġ
    public bool isChasing = false;
    public LayerMask playerLayer;
    public Monster monster;
    public Animator animator;

    private void Awake() {
        monster = GetComponent<Monster>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDetect();
        if (isChasing && targetPlayer != null) {
            Chace();
        }
    }
    public void Chace() {
        if (targetPlayer == null) {
            return;
        }
        Vector2 direction = (targetPlayer.position - transform.position).normalized;
        monster.Move(direction);
    }
    public void PlayerDetect() {
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, monster.searchRange, playerLayer);
        if (players.Length > 0) {
            // ���� ����� �÷��̾ Ÿ������ ����
            targetPlayer = players[0].transform;
            isChasing = true;
        }
        else {
            // Ž�� ���� ���� �÷��̾ ������ ���� ����
            targetPlayer = null;
            isChasing = false;
            //animator.SetInteger("AnimState", 0);
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.white;
        if (monster != null) {
            Gizmos.DrawWireSphere(transform.position, monster.searchRange); //���� Ž�� ����
        }
    }
}
