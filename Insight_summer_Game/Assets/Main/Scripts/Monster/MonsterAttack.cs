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
        // �ڽ� Ŭ�������� �� ������ ������Ʈ ��������

        public virtual IEnumerator AttackCoroutine()
        {
            Attack();
            yield return AttackDelay;
        }

        public virtual void Attack()
        {
            Debug.Log("���Ͱ� �÷��̾ ����");
            player.Hit();
            anim.SetTrigger("Attack");
        }

        private void OnEnable()
        {
            player = GameObject.Find("Player").GetComponent<HeroKnight>();
        }
    }


}