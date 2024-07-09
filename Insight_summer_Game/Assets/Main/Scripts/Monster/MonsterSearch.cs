using UnityEditor.Rendering;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Events;

namespace Monster
{
    public class MonsterSearch : MonoBehaviour
    {
        public event UnityAction PlayerFound;
        public event UnityAction PlayerUnfound;

        public Transform Target { get; private set; }
        private float searchRange = 5.0f; // 몬스터마다 다르게 수정하기

        public virtual void Search()
        {
            RaycastHit2D raycastHit = Physics2D.CircleCast(transform.position, searchRange, Vector2.down, 0, LayerMask.GetMask("Player"));
            if (raycastHit.collider != null)
            {
                Target = raycastHit.transform;
                PlayerFound?.Invoke();
            }
            else
            {
                Target = null;
                PlayerUnfound?.Invoke();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red; 
            Gizmos.DrawWireSphere(transform.position, searchRange); // 원형 기즈모
        }
    }
}