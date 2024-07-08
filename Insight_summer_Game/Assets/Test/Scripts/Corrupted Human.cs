using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CorruptedHuman : Monster
{
    [Header("Components")]
    public Rigidbody2D monsterRigidbody;
    public SpriteRenderer spriteRenderer;
    public Collider2D col;
    public Animator animator;

    [Header("Settings")]
    public LayerMask playerLayer;
    public  Transform attackPoint;

    private Transform targetPlayer; // �̵��� �÷��̾��� ��ġ
    private bool isChasing = false;

    void Awake() {
        monsterRigidbody = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Start() {
        isLive = true;
        maxHealth = 100;
        curhealth = maxHealth;
        speed = 2;
        attackPower = 10;
        attackSpeed = 1;
        attackRange = new Vector2(3,1); 
        searchRange = 3;//�ӽ÷� Vector2���� float�� ��������
    }



    void Update()
    {
        CheckAttackRange();
        PlayerDetect();
        if (Input.GetMouseButtonDown(0)) {
            Attack();
        }
        else if (Input.GetMouseButtonDown(1)) {
            TakeDamage(30);
        }
        if (isChasing && targetPlayer != null) {
            Chace();   
        }
    }   
    public override void Attack() {
        animator.SetTrigger("Attack1");
    }

    public override void Chace() {
        Vector2 direction = (targetPlayer.position - transform.position).normalized;
        monsterRigidbody.velocity = new Vector2(direction.x * speed, monsterRigidbody.velocity.y);

        animator.SetInteger("AnimState", 1);

        if (direction.x > 0) {
            //spriteRenderer.flipX = false;
            transform.localScale = new Vector3(1f, 1f, 1f);

        }
        else if (direction.x < 0) {
            //spriteRenderer.flipX = true;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    public override void Die() {
        isLive = false;
    }

    public override void Idel() {
        // ��� ���� �ڵ�
    }

    public override void Move() {
        //�����̴� �ڵ�
    }

    public override void TakeDamage(float damage) {
        if (!isLive) {
            return;
        }
        curhealth -= damage;
        if(curhealth <= 0) { 
            Die(); 
        }
        animator.SetTrigger("Hit");
    }

    private void PlayerDetect() { // ���� �����ȿ� �÷��̾ �ִ��� Ž��
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, searchRange, playerLayer);
        if (players.Length > 0) {
            // ���� ����� �÷��̾ Ÿ������ ����
            targetPlayer = players[0].transform;
            isChasing = true;
        }
        else {
            // Ž�� ���� ���� �÷��̾ ������ ���� ����
            targetPlayer = null;
            isChasing = false;
            animator.SetInteger("AnimState", 0);
        }
    }

    private void CheckAttackRange() { // ���ݰ��� ���� �ȿ� �÷��̾ �ִ��� Ž��
        Collider2D playerInRange = Physics2D.OverlapBox(transform.position, attackRange, 0, playerLayer);
        if (playerInRange != null && playerInRange.CompareTag("Player")) {
            Attack();
        }
    }

    public void DetectAttack() {
        if (attackPoint != null) {
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange.y/2, playerLayer);
            foreach (Collider2D enemy in hitPlayer) {
                Debug.Log("�÷��̾� ����");
                enemy.GetComponent<Player>().TakeDamage(attackPower);
            }
        }
        
    }

    void OnDrawGizmosSelected() { 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRange); //���� Ž�� ����
        Gizmos.DrawWireCube(transform.position, attackRange);// ���� Ž�� ����

        if (attackPoint != null) {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange.y / 2); //���� ���� ����
        }
    }
}
