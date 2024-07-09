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

    //Monster Behaviors(Method)
    public abstract void Attack();
   public abstract void Contact();
   public abstract void Hit();
   public abstract void Dead();
   public abstract void Idle();
   public abstract void Walk();
   public abstract void Chase();
   public abstract void Search();


}
