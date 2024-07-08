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

    private Transform targetPlayer; // 이동할 플레이어의 위치
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
        searchRange = 3;//임시로 Vector2에서 float로 변경했음
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
        // 대기 상태 코드
    }

    public override void Move() {
        //움직이는 코드
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

    private void PlayerDetect() { // 추적 범위안에 플레이어가 있는지 탐색
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, searchRange, playerLayer);
        if (players.Length > 0) {
            // 가장 가까운 플레이어를 타겟으로 설정
            targetPlayer = players[0].transform;
            isChasing = true;
        }
        else {
            // 탐지 범위 내에 플레이어가 없으면 추적 중지
            targetPlayer = null;
            isChasing = false;
            animator.SetInteger("AnimState", 0);
        }
    }

    private void CheckAttackRange() { // 공격가능 범위 안에 플레이어가 있는지 탐색
        Collider2D playerInRange = Physics2D.OverlapBox(transform.position, attackRange, 0, playerLayer);
        if (playerInRange != null && playerInRange.CompareTag("Player")) {
            Attack();
        }
    }

    public void DetectAttack() {
        if (attackPoint != null) {
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange.y/2, playerLayer);
            foreach (Collider2D enemy in hitPlayer) {
                Debug.Log("플레이어 맞음");
                enemy.GetComponent<Player>().TakeDamage(attackPower);
            }
        }
        
    }

    void OnDrawGizmosSelected() { 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRange); //추적 탐지 범위
        Gizmos.DrawWireCube(transform.position, attackRange);// 공격 탐지 범위

        if (attackPoint != null) {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange.y / 2); //공격 판정 범위
        }
    }
}
