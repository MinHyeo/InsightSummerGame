using System;
using UnityEngine;

//Monster Abstract class
public abstract class Monster : MonoBehaviour
{
    //These veriables are initialized at inspector
    [Header("Monster Status")]
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float speed;
    [SerializeField] protected float attackPower;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float searchRange;

    [Header("Monster Component")]
    [SerializeField] protected Animator monsterAnimator;
    [SerializeField] protected Collider2D monsterColl;
    [SerializeField] protected Rigidbody2D monsterRigid;
    [SerializeField] protected SpriteRenderer monsterSpriteRenderer;

    [Header("Target Player Info")]
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected Transform targetPlayer;


    private void Awake()
    {
        monsterAnimator = GetComponent<Animator>();
        monsterColl = GetComponent<Collider2D>();
        monsterRigid = GetComponent<Rigidbody2D>();
        monsterSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Monster Behaviors(Method)
    public abstract void Attack();
    public abstract void Contact();
    public abstract void Hit();
    public abstract void Dead();
    public abstract void Idle();
    public abstract void Walk();
    public abstract void Chase();
    public void Search() {
        //탐지 범위 생성
        targetPlayer = (Physics2D.CircleCast(transform.position, searchRange, Vector2.zero, 0, playerLayer)).transform;
        //플레이어를 찾았을 경우
        if (targetPlayer != null)
        {
            Debug.Log("Player Detacted!!");
            if (Vector2.Distance(transform.position, targetPlayer.position) <= attackRange)
            {
                Attack();
            }
            Chase();
        }
        else
        {
            monsterAnimator.SetBool("IsDetacted", false);
        }
    }

    void OnDrawGizmosSelected()
    {
        //추적 범위
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, searchRange);
        //공격 범위
        Gizmos.color = Color.red;
        Vector3 attackDistance = new Vector3(transform.position.x - attackRange, transform.position.y, transform.position.z);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
