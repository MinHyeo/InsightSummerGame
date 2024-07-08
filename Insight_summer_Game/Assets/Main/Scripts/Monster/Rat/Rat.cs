using UnityEngine;
using System.Collections;

namespace Monster.Rat
{
    [RequireComponent(typeof(RatMovement), typeof(MonsterSearch), typeof(RatAttack))]
    [RequireComponent (typeof(CheckAttackable), typeof(MonsterHit))]
    public class Rat : Monster
    {
        [SerializeField] private RatMovement ratMovement;
        [SerializeField] private MonsterSearch monsterSearch;
        [SerializeField] private RatAttack ratAttack;
        [SerializeField] private CheckAttackable checkAttackable;
        [SerializeField] private MonsterHit monsterHit;

        private Collider2D attackPoint;

        private void Awake()
        {
            ratMovement = GetComponent<RatMovement>();
            monsterSearch = GetComponent<MonsterSearch>();
            ratAttack = GetComponent<RatAttack>();

            pos = GetComponent<Transform>();


            monsterSearch.PlayerFound += OnPlayerFound;
            monsterSearch.PlayerUnfound += OnPlayerUnfound;
        }

        private void Start()
        {
            Init();
        }
        protected override void Init()
        {
            checkAttackable = GetComponentInChildren<CheckAttackable>();
            checkAttackable.PlayerAttacked += OnPlayerAttack;
            checkAttackable.PlayerUnAttacked += OnPlayerUnAttack;

            MonsterState = State.Idle;
            maxHealth = 100.0f;
            moveSpeed = 3.0f;
            attackRange = 0.3f;
            attackSpeed = 1.0f;
            attackPower = 10.0f;

            ratMovement.Speed = moveSpeed;
        }
        private void Update()
        {

            switch (MonsterState)
            {
                case State.Idle:
                    monsterSearch.Search();
                    break;
                case State.Patrol:
                    monsterSearch.Search();
                    ratMovement.Patrol();
                    break;
                case State.Chase:
                    monsterSearch.Search();
                    ratMovement.Chase();
                    break;
                case State.Attack:
                    StartCoroutine(ratAttack.AttackCoroutine());
                    break;
                case State.Hit:
                    monsterSearch.Search();
                    
                    break;
                case State.Die:
                    break;
            }

        }

        private void OnPlayerFound()
        {
            this.target = monsterSearch.Target;
            ratMovement.Target = target;
            MonsterState = State.Chase;
        }
        private void OnPlayerUnfound()
        {
            this.target = null;
            ratMovement.Target = null;
            MonsterState = State.Patrol;
        }
        private void OnPlayerAttack()
        {
            MonsterState = State.Attack;
        }
        private void OnPlayerUnAttack()
        {
            if(this.target == null) { MonsterState = State.Patrol; }
            else { MonsterState = State.Chase; }
        }
        private void OnDrawGizmos()
        {
            // 공격 범위를 나타내는 원을 그립니다.
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}