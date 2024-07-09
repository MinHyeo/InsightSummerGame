using Monster.Rat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class MonsterAttack : MonoBehaviour
    {
        protected Animator anim;
        protected HeroKnight player;
        protected float AttackDelay;
        // 자식 클래스에서 각 몬스터의 컴포넌트 가져오기

        public virtual IEnumerator AttackCoroutine()
        {
            Attack();
            yield return AttackDelay;
        }

        public virtual void Attack()
        {
            Debug.Log("몬스터가 플레이어를 공격");
            player.Hit();
            anim.SetTrigger("Attack");
        }

        private void OnEnable()
        {
            player = GameObject.Find("Player").GetComponent<HeroKnight>();
        }
    }


}