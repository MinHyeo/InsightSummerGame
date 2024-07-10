using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mishroom : Monster
{
    [Header("Components")]
    [Serialize] Rigidbody2D monsterRigidbody;
    [Serialize] SpriteRenderer spriteRenderer;
    [Serialize] Collider2D col;
    [Serialize] Animator animator;

    [Header("Settings")]
    public LayerMask playerLayer;

    private Transform targetPlayer; // 이동할 플레이어의 위치




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
        speed = 1f;

        attackPower = 10f;
        attackSpeed = 1f;
        attackRange = 0.5f;
        attackDirection = new Vector2(1, 0);
        attackDistance = 0.7f;

        searchRange = 2f;//임시로 Vector2에서 float로 변경했음
    }

    // Update is called once per frame
    void Update()
    {
        if (monsterRigidbody.velocity == Vector2.zero) {
            animator.SetInteger("AnimState", 0);
        }
    }
    public override void TakeDamage(float damage) {
        base.TakeDamage(damage);
        animator.SetTrigger("Hit");
    }

    public override void Die() {
    }

    public override void Idle() {
    }

    public override void Move(Vector2 direction) {
        monsterRigidbody.velocity = new Vector2(direction.x * speed, monsterRigidbody.velocity.y);

        animator.SetInteger("AnimState", 1);
        if (direction.x > 0) {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0) {
            spriteRenderer.flipX = true;
        }

    }
    void OnDrawGizmosSelected() {
        Vector2 start = transform.position;
        Vector2 end = start + attackDirection.normalized * attackDistance;
        float radius = attackRange;
        Gizmos.color = Color.red;
        // 시작점 원형 기즈모
        Gizmos.DrawWireSphere(start, radius);
        // 끝점 원형 기즈모
        Gizmos.DrawWireSphere(end, radius);
    }
}
