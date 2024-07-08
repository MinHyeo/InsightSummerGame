using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.Skeleton
{
    public class Skelton : Monster
    {
        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
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
    }
}

