using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [Header("Stats")]
    public bool isLive;
    public float maxHealth;
    public float curhealth;
    public float speed;
    public float attackPower;
    public float attackSpeed;

    public float attackRange;
    public float attackDistance;
    public Vector2 attackDirection;

    public float searchRange;

    public abstract void Attack();
    public abstract void Die();

    public abstract void TakeDamage(float damage);
    public abstract void Move();
    public abstract void Chace();
    public abstract void Idel();
    
}
