using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;


public abstract class Monster : MonoBehaviour
{
    public float maxHealth;
    public float moveSpeed;
    public float attackPower;
    public float attackRange;
    public float attackSpeed;
    public float searchRange;

    protected Rigidbody2D rb;
    protected Animator animator;
    protected Sprite sprite;

    public virtual void Init()
    {
        rb =        GetComponent<Rigidbody2D>();
        animator =  GetComponent<Animator>();
        sprite =    GetComponent<Sprite>();

    }
    public abstract void Hit();
    public abstract void Die();
    public abstract void Move();
    public abstract void Attack();

}

