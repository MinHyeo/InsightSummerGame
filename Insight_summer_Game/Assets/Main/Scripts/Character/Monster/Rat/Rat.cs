using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RatMove))]
public class Rat : Monster
{
    [SerializeField] private RatMove ratmove;
        public override void Init()
        {
            maxHealth =     5.0f;
            moveSpeed =     5.0f;
            attackPower =   1.0f;
            attackRange =   0.5f;
            attackSpeed =   3.0f;
            searchRange =   10.0f;
        }

        public override void Move()
        {
            
        }
        public override void Attack()
        {

        }
        public override void Hit()
        {

        }
        public override void Die()
        {
            
        }
}
