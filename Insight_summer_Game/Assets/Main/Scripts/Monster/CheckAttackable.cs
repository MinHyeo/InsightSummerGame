using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.PlayerSettings;

namespace Monster
{
    public class CheckAttackable : MonoBehaviour
    {
        public event UnityAction PlayerAttacked;
        public event UnityAction PlayerUnAttacked;

        private Transform pos;
        protected float attackRange; // 각 몬스터의 공격범위 받아오기

        private void Awake()
        {
            pos = GetComponent<Transform>();
        }

        private void Update()
        {
            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(pos.position, 0.3f); // 나중에 AttackRange로 수정.
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.name == "Player")
                {
                    PlayerAttacked?.Invoke();
                }
                else
                {
                    PlayerUnAttacked?.Invoke();
                }
            }
        }

    }
}