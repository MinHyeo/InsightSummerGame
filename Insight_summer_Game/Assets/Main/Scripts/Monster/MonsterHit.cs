using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Monster
{
    public class MonsterHit : MonoBehaviour
    {
        Sprite sprite;
        Rigidbody2D rigid;
        public Transform player;
        private Transform transform;
        private Animator anim;

        public bool isHit;
        public bool isStunned;
    
        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            transform = GetComponent<Transform>();
            sprite = GetComponent<Sprite>();
            anim = GetComponent<Animator>();
        }

        public virtual void Hit()
        {
            float knockbackForce = 10.0f;
            float comparePosition = transform.position.x - player.position.x;
            int knockbackDir = comparePosition > 0? 1 : -1; // 나중에 넉백 변수 쓰기

            rigid.velocity = new Vector2(knockbackDir * knockbackForce, 2);
            //transform.position = Vector2.MoveTowards(transform.position, new Vector3(knockbackDir * knockbackDir, 1, 0), 10 * Time.deltaTime);
            //rigid.AddForce(new Vector2(knockbackDir * knockbackForce, 1), ForceMode2D.Force);

            isHit = true;
            anim.SetTrigger("Hit");
        }

        private void OnEnable()
        {
            player = GameObject.Find("Player").GetComponent<Transform>();
        }
    }
}

