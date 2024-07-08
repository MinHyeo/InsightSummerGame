using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Monster
{
    public abstract class Monster : MonoBehaviour
    {
        public enum State
        {
            Idle,
            Patrol,
            Chase,
            Attack,
            Hit,
            Die
        }

        public State MonsterState;

        public float maxHealth;
        public float moveSpeed;
        public float attackPower;
        public float attackRange;
        public float attackSpeed;
        public float searchRange;

        protected Rigidbody2D rb;
        protected Animator animator;
        protected Sprite sprite;
        protected Transform target;
        protected Transform pos;

        protected abstract void Init();
    }
}

