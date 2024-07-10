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

    //public abstract void Attack();
    public abstract void Die();

    public virtual void TakeDamage(float damage) {
        if (!isLive) {
            return;
        }
        curhealth -= damage;
        if (curhealth <= 0) {
            Die();
        }
    }
    public abstract void Move(Vector2 direction);
    //public abstract void Chace();
    public  abstract void Idle();
    
}
