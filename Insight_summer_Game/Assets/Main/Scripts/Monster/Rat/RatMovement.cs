using System;
using UnityEngine;

namespace Monster.Rat
{
    public class RatMovement : MonoBehaviour
    {
        Rigidbody2D rigid;
        SpriteRenderer sprite;
        Animator animator;
        Transform pos;
        public Transform Target { private get; set; }
        public float Speed { private get; set; }

        private int nextDir;
        private int changeTime;
        private int dir;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            pos = GetComponent<Transform>();
        }
        private void Start()
        {
            ChangeDirection();
        }
    

        public void Patrol()
        {
            sprite.flipX = rigid.velocity.x > 0f ? true : false;
            rigid.velocity = new Vector2(nextDir * Speed, rigid.velocity.y);

        }
        public void Chase()
        {
            if (Target != null)
            {
                dir = Target.position.x < transform.position.x ? -1 : 1;
                sprite.flipX = rigid.velocity.x > 0f ? true : false;
                rigid.velocity = new Vector2(dir * Speed, rigid.velocity.y);
            }

        }
        private void ChangeDirection()
        {
            changeTime = UnityEngine.Random.Range(1, 4);
            nextDir = Normalize(UnityEngine.Random.Range(-5, 5));
            Invoke("ChangeDirection", changeTime);
        }
        private int Normalize(float value)
        {
            if (value < -1.67f) return -1;
            else if (value > 1.67f) return 1;
            else return 0;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Barrier"))
            {
                nextDir *= -1;
            }
        }

    }
}