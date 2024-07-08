using System.Collections;
using UnityEngine;

namespace Monster
{
    interface IAttackable 
    {
        public void CheckAttackRange();
        public void Attack();
    }

    public abstract class Monster : MonoBehaviour
    {
        protected enum State
        {
            Idle,
            Move,
            Search,
            Chase,
            Attack,
            Dead
        }
        [SerializeField]protected State state;
        protected float health;
        protected float speed;
        protected float attackPower;
        protected float attackSpeed;
        protected Vector2 attackRange;
        protected Vector2 searchRange;
        protected int thinkTime;
        protected int nextMove;

        protected SpriteRenderer sprite;
        protected Rigidbody2D rigid;
        protected Animator anim;

        protected Transform target;

        protected void Think()
        {
            nextMove = Random.Range(-1, 2);
            if(nextMove != 0)
                state = State.Move;
            else
                state = State.Idle;

            thinkTime = Random.Range(3, 5);
            Invoke("Think", thinkTime);
        }
        protected void Idle()
        {
            //Animation Part
        }
        protected virtual void Movement()
        {
            sprite.flipX = nextMove == 1;

            Vector2 frontVec = new Vector2(transform.position.x + nextMove * 0.5f, transform.position.y);
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector2.down, 1, LayerMask.GetMask("Ground"));
            if (rayHit.collider == null)
                nextMove *= -1;
            rigid.MovePosition(new Vector2(transform.position.x + nextMove * speed * Time.deltaTime, transform.position.y));

            //Animation Part    
        }
        protected virtual void Search()
        {
            RaycastHit2D rayHit = Physics2D.CircleCast(transform.position, searchRange.x, Vector2.down, searchRange.y, LayerMask.GetMask("Player"));
            if (rayHit.collider != null)
            {
                Debug.Log("Search");
                state = State.Chase;
                target = rayHit.transform;
                CancelInvoke("Think");
            }
            else
            {
                if (state != State.Chase && state != State.Attack)
                    return;

                state = State.Idle;
                target = null;
                Invoke("Think", thinkTime);
            }
        }
        protected virtual void Chase()
        {
            Debug.Log("Chase");
            Vector2 targetDir = target.position - transform.position;
            Vector2 fixedDir = new Vector2(
            targetDir.x > 0 ? 1 : (targetDir.x < 0 ? -1 : 1),
            targetDir.y > 0 ? 1 : (targetDir.y < 0 ? -1 : 1)
            );
            sprite.flipX = targetDir.x > 0;
            rigid.MovePosition(new Vector2(transform.position.x + fixedDir.x * speed * Time.deltaTime, transform.position.y));
        }
        public virtual void Hit()
        {
            Debug.Log("몬스터 공격 당함");
            if (health <= 0)
                Dead();

            StartCoroutine(OnHit());
            health -= 10;
        }
        IEnumerator OnHit()
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sprite.color = Color.white;
        }
        protected void Dead()
        {
            state = State.Dead;
            gameObject.SetActive(false);
        }
    }
}