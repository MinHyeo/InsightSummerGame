using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CorruptedHuman : Monster
{
    [Header("Components")]
    [Serialize] Rigidbody2D monsterRigidbody;
    [Serialize] SpriteRenderer spriteRenderer;
    [Serialize] Collider2D col;
    [Serialize] Animator animator;

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
        speed = 2f;
        attackPower = 10f;
        attackSpeed = 1f;
        attackRange = 0.5f;
        attackDirection = new Vector2(1, 0);
        attackDistance = 1f;
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
        if (targetPlayer == null) {
            return;
        }
        Vector2 direction = (targetPlayer.position - transform.position).normalized;
        monsterRigidbody.velocity = new Vector2(direction.x * speed, monsterRigidbody.velocity.y);

        animator.SetInteger("AnimState", 1);

        if (direction.x > 0) {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0) {
            spriteRenderer.flipX = true;
        }

        attackDirection = spriteRenderer.flipX ? new Vector2(-Mathf.Abs(attackDirection.x), attackDirection.y)
                                               : new Vector2(Mathf.Abs(attackDirection.x), attackDirection.y);


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
        RaycastHit2D hitPlayer = Physics2D.CircleCast(transform.position, attackRange, attackDirection, attackDistance,playerLayer);
        if (hitPlayer.collider != null) {
            Debug.Log("���� �����ȿ� �÷��̾� ����");
            Attack();
        }
    }

    public void DetectAttack() {
        RaycastHit2D hitPlayer = Physics2D.CircleCast(transform.position, attackRange, attackDirection, attackDistance, playerLayer);
        if(hitPlayer.collider != null) {
            Debug.Log("�÷��̾� hit");
            hitPlayer.collider.GetComponent<Player>().TakeDamage(attackPower);
        }
        
    }

    void OnDrawGizmosSelected() { 
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, searchRange); //���� Ž�� ����


        Vector2 start = transform.position;
        Vector2 end = start + attackDirection.normalized * attackDistance;
        float radius = attackRange;
        Gizmos.color = Color.red;
        // ������ ���� �����
        Gizmos.DrawWireSphere(start, radius);
        // ���� ���� �����
        Gizmos.DrawWireSphere(end, radius);
    }
}
