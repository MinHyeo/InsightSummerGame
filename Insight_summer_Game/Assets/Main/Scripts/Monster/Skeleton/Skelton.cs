using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.Skeleton
{
    public class Skelton : Monster, IAttackable
    {
        private float attackDelayTime;

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
            attackDelay = 1;
            attackDelayTime = attackDelay;
            searchRange = new Vector2(5, 5);
            Think();
        }
        private void FixedUpdate()
        {
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
        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireCube(transform.position + Vector3.right * nextMove, attackRange);
        //    Gizmos.color = Color.blue;
        //    Gizmos.DrawWireCube(transform.position, searchRange);
        //}
        //protected override void Search()
        //{
        //    base.Search();
        //    if (isSearch)
        //    {
        //        if (state == State.Attack)
        //            return;

        //        state = State.Chase;
        //    }
        //}
        protected override void Chase()
        {
            base.Chase();
            CheckAttackRange();
        }
        public void CheckAttackRange()
        {
            int mosterFront = sprite.flipX == true ? 1 : -1;
            RaycastHit2D playerHit = Physics2D.Raycast(transform.position, Vector3.right * mosterFront, attackRange.x,LayerMask.GetMask("Player"));
            if (playerHit.collider != null)
            {
                state = State.Attack;
                Attack();
            }
        }
        public override void StartAttack()
        {
            throw new System.NotImplementedException();
        }
        public override void Attack()
        {
            if (attackDelayTime < attackDelay)
            {
                attackDelayTime += Time.deltaTime;
                return;
            }
            attackDelayTime = 0;

            //Animation Part
            anim.SetTrigger("Attack");
            
            Collider2D[] player = Physics2D.OverlapBoxAll(transform.position + Vector3.right * nextMove, attackRange, 0, LayerMask.GetMask("Player"));
            foreach (var target in player)
                target.GetComponent<HeroKnight>().Hit(attackPower);
        }
        public void AttackEnd()
        {
            state = State.Chase;
        }
    }
}