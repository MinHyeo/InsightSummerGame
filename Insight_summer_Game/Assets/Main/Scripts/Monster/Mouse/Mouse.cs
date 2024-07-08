using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.Mouse
{
    public class Mouse : Monster
    {
        private void Awake()
        {
            sprite = GetComponent<SpriteRenderer>();
            rigid = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            Init();
        }
        void Init()
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
                    Dead();
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
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<HeroKnight>().Hit(attackPower);
            }
        }
        protected override void Chase()
        {
            base.Chase();

        }
    }
}
