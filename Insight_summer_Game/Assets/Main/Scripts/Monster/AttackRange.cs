using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster 
{
    public class AttackRange : MonoBehaviour
    {
        BoxCollider2D attackRange;
        Monster monsterScript;

        private void Awake()
        {
            attackRange = GetComponent<BoxCollider2D>();
            monsterScript = GetComponentInParent<Monster>();
        }
        public void AttackRangeInit(Vector2 attackRange, Vector2 attackRangePos)
        {
           this.attackRange.size = attackRange;
           this.attackRange.offset = attackRangePos;
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("몬스터가 플레이어 공격 시작");
                monsterScript.StartAttack();
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                monsterScript.StopAttack();
            }
        }
    }
}