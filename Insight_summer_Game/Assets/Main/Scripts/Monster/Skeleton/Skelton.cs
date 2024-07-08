using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.Skeleton
{
    public class Skelton : Monster, IAttackable
    {
        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
        }
        private void Start()
        {
            Init();
        }
        private void Init()
        {
            state = State.Idle;
            health = 100;
            speed = 2;
            attackPower = 10;
            attackSpeed = 1;
            attackRange = new Vector2(1, 1);
            searchRange = new Vector2(5, 5);
            Think();
        }
        private void Update()
        {
            switch (state)
            {
                case State.Attack:
                    //Attack();
                    break;
                case State.Dead:
                    //Dead();
                    break;
            }
        }
        private void FixedUpdate()
        {
            Search();
            switch (state)
            {
                case State.Idle:
                    Idle();
                    break;
                case State.Move:
                    Movement();
                    break;
                case State.Chase:
                    Chase();
                    break;
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + Vector3.right * nextMove, attackRange);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, searchRange);
        }
        protected override void Chase()
        {
            base.Chase();
        }
        public void CheckAttackRange()
        {
            RaycastHit2D playerhit = Physics2D.BoxCast(transform.position, attackRange, 0, Vector2.right * nextMove, LayerMask.GetMask("Player"));
            if (playerhit.collider != null)
            {
                state = State.Attack;
                Attack();
            }
        }
        public void Attack()
        {
            //Animation Part
            anim.SetTrigger("Attack");
            
            Collider2D[] player = Physics2D.OverlapBoxAll(transform.position + Vector3.right * nextMove, attackRange, 0, LayerMask.GetMask("Player"));
            foreach (var target in player)
                target.GetComponent<HeroKnight>().Hit(attackPower);
            state = State.Chase;
        }
    }
}

