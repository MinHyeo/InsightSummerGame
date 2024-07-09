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
