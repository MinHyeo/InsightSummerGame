using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Monster
{
    [Header("Components")]
    public Rigidbody2D monsterRigidbody;
    public SpriteRenderer spriteRenderer;
    public Collider2D col;
    public Animator animator;

    [Header("Settings")]
    public LayerMask playerLayer;

    private Transform targetPlayer; // 이동할 플레이어의 위치
    private bool isChasing = false;

    private void Awake() {
        monsterRigidbody = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        isLive = true;
        maxHealth = 100;
        curhealth = maxHealth;
        speed = 4;
        attackPower = 10;
        attackSpeed = 0;
        attackRange = 2f;
        searchRange = 3;//임시로 Vector2에서 float로 변경했음
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDetect();
        if (isChasing && targetPlayer != null) {
            Chace();
        }
    }

    public override void Attack() {
        throw new System.NotImplementedException();
    }

    public override void Chace() {
        Vector2 direction = (targetPlayer.position - transform.position).normalized;
        monsterRigidbody.velocity = new Vector2(direction.x * speed, monsterRigidbody.velocity.y);

        if (direction.x > 0) {
            //spriteRenderer.flipX = false;
            transform.localScale = new Vector3(1f, 1f, 1f);

        }
        else if (direction.x < 0) {
            //spriteRenderer.flipX = true;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void PlayerDetect() {
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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null) {
                player.TakeDamage(attackPower);
            }
        }
    }

    public override void Die() {
        throw new System.NotImplementedException();
    }

    public override void Idel() {
        throw new System.NotImplementedException();
    }

    public override void Move() {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(float damage) {
        throw new System.NotImplementedException();
    }


}
