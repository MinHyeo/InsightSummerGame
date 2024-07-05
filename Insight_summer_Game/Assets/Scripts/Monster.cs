using System;
using UnityEngine;

//Monster Abstract class
public abstract class Monster : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Monster Status")]
    public float currentHealth;
    public float maxHealth;
    public float speed;
    public float attackPower;
    public float attackRange;
    public float searchRange;

   //Monster Behaviors(Method)
   public abstract void Attack();
   public abstract void Contact();
   public abstract void Hit();
   public abstract void Dead();
   public abstract void Idle();
   public abstract void Walk();
   public abstract void Chase();

}
