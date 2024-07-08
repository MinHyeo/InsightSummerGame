using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.Rat
{
    public class RatAttack : MonoBehaviour
    {
        Animator anim;
        Rat rat;
        public HeroKnight player;
        private void Awake()
        {
            anim = GetComponent<Animator>();
            rat = GetComponent<Rat>();
        }
        //modify Hardcord
        WaitForSeconds AttackDelay = new WaitForSeconds(3f);
        public IEnumerator AttackCoroutine()
        {
            if (rat.MonsterState != Monster.State.Attack) { StopCoroutine(AttackCoroutine()); }
            Attack();
            yield return AttackDelay;
        }

        public void Attack()
        {
            Debug.Log("���Ͱ� �÷��̾ ����");
            player.Hit();
            //anim.SetTrigger("Attack");
        }
    }
}