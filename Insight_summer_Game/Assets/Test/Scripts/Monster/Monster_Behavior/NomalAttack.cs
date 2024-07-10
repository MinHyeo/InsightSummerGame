using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NomalAttack : MonoBehaviour, IAttackable
{
    public float attackPower;
    public float attackSpeed;
    public float attackRange;
    public float attackDistance;
    public Vector2 attackDirection;
    public LayerMask playerLayer;

    private bool isAttacking = false;
    
    public Monster monster;
    public Animator anim;
    [Serialize] SpriteRenderer spriteRenderer;
    private void Awake() {
        monster = GetComponent<Monster>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start() {
        attackPower = 10f;
        attackSpeed = 1f;
        attackRange = 0.5f;
        attackDirection = new Vector2(1, 0);
        attackDistance = 1f;
    }
    private void Update() {
        if (!isAttacking) {
            CheckAttackRange();
        }
        attackDirection = spriteRenderer.flipX ? new Vector2(-Mathf.Abs(attackDirection.x), attackDirection.y)
                                               : new Vector2(Mathf.Abs(attackDirection.x), attackDirection.y);
    }

    public void Attack() {
        anim.SetTrigger("Attack1");
        isAttacking = true;
    }

    public void CheckAttackRange() {
        RaycastHit2D hitPlayer = Physics2D.CircleCast(transform.position, attackRange, attackDirection, attackDistance, playerLayer);
        if (hitPlayer.collider != null) {
            Debug.Log("공격 범위안에 플레이어 있음");
            Attack();
        }
    }

    public void DetectAttack() {
        RaycastHit2D hitPlayer = Physics2D.CircleCast(transform.position, attackRange, attackDirection, attackDistance, playerLayer);
        if (hitPlayer.collider != null) {
            Debug.Log("플레이어 hit");
            hitPlayer.collider.GetComponent<Player>().TakeDamage(attackPower);
        }
    }

    public void EndAttack() {
        isAttacking = false;
    }

}
