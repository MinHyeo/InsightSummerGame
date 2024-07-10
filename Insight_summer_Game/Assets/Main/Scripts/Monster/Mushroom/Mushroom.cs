using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.Mushroom 
{
    public class Mushroom : Monster, IAttackable
    {
        AttackRange attackRangeScript;
        Vector2 attackRangePos;
        float attackDelayTime;

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
            attackRange = new Vector2(0.5f, 0.3f);
            attackRangePos = new Vector2(0, -0.1f);
            attackRangeScript = GetComponentInChildren<AttackRange>();
            attackRangeScript.AttackRangeInit(attackRange, attackRangePos);
            attackDelay = 1;
            attackDelayTime = attackDelay;
            searchRange = new Vector2(4, 2);
            playerScanner = GetComponentInChildren<PlayerScanner>();
            playerScanner.ScannerInit(searchRange);
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
        protected override void Idle()
        {
            base.Idle();
            anim.SetFloat("speed", 0);
        }
        protected override void Movement()
        {
            base.Movement();

            anim.SetFloat("speed", Mathf.Abs(nextMove));
        }
        protected override void Chase()
        {
            base.Chase();
            anim.SetFloat("speed", 1);
        }
        public void CheckAttackRange()
        {
            int mosterFront = sprite.flipX == true ? 1 : -1;
            RaycastHit2D playerHit = Physics2D.Raycast(transform.position, Vector3.right * mosterFront, attackRange.x, LayerMask.GetMask("Player"));
            if (playerHit.collider != null)
            {
                state = State.Attack;
                Attack();
            }
        }
        public override void StartAttack()
        {
            if (attackDelayTime < attackDelay)
            {
                attackDelayTime += Time.deltaTime;
                return;
            }
            attackDelayTime = 0;
            state = State.Attack;

            sprite.flipX = target.position.x > transform.position.x;

            //Animation Part
            anim.SetTrigger("Attack");
        }
        public override void Attack()
        {
            Collider2D[] player = Physics2D.OverlapBoxAll(transform.position + Vector3.right * nextMove, attackRange, 0, LayerMask.GetMask("Player"));
            foreach (var target in player)
                target.GetComponent<HeroKnight>().Hit(attackPower);
        }
        public override void StopAttack()
        {
            state = State.Idle;
        }
    }
}