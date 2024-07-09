using UnityEngine;
using System.Collections;

namespace Monster.Slime
{
    [RequireComponent(typeof(SlimeMovement), typeof(SlimeSearch), typeof(SlimeAttack))]
    [RequireComponent(typeof(SlimeCheckATK), typeof(SlimeHit))]
    public class Slime : Monster
    {
        [SerializeField] private SlimeMovement slimeMovement;
        [SerializeField] private SlimeSearch slimeSearch;
        [SerializeField] private SlimeAttack slimeAttack;
        [SerializeField] private SlimeCheckATK slimeCheckATK;
        [SerializeField] private SlimeHit slimeHit;

        private Collider2D attackPoint;

        private void Awake()
        {
            slimeMovement = GetComponent<SlimeMovement>();
            slimeSearch = GetComponent<SlimeSearch>();
            slimeAttack = GetComponent<SlimeAttack>();

            pos = GetComponent<Transform>();


            slimeSearch.PlayerFound += OnPlayerFound;
            slimeSearch.PlayerUnfound += OnPlayerUnfound;
        }

        private void Start()
        {
            Init();
        }
        protected override void Init()
        {
            slimeCheckATK = GetComponentInChildren<SlimeCheckATK>();
            slimeCheckATK.PlayerAttacked += OnPlayerAttack;
            slimeCheckATK.PlayerUnAttacked += OnPlayerUnAttack;

            MonsterState = State.Idle;
            maxHealth = 100.0f;
            moveSpeed = 1.3f;
            attackRange = 0.3f;
            attackSpeed = 1.0f;
            attackPower = 10.0f;

            slimeMovement.Speed = moveSpeed;
        }
        private void Update()
        {

            switch (MonsterState)
            {
                case State.Idle:
                    slimeSearch.Search();
                    break;
                case State.Patrol:
                    slimeSearch.Search();
                    slimeMovement.Patrol();
                    break;
                case State.Chase:
                    slimeSearch.Search();
                    slimeMovement.Chase();
                    break;
                case State.Attack:
                    StartCoroutine(slimeAttack.AttackCoroutine());
                    break;
                case State.Hit:
                    slimeSearch.Search();

                    break;
                case State.Die:
                    break;
            }

        }

        private void OnPlayerFound()
        {
            this.target = slimeSearch.Target;
            slimeMovement.Target = target;
            MonsterState = State.Chase;
        }
        private void OnPlayerUnfound()
        {
            this.target = null;
            slimeMovement.Target = null;
            MonsterState = State.Patrol;
        }
        private void OnPlayerAttack()
        {
            MonsterState = State.Attack;
        }
        private void OnPlayerUnAttack()
        {
            if (this.target == null) { MonsterState = State.Patrol; }
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