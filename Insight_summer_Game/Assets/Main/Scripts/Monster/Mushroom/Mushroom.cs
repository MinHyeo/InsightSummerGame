using UnityEngine;
using System.Collections;
using Monster.Slime;

namespace Monster.Mushroom
{
    [RequireComponent(typeof(MushroomMovement), typeof(MushroomSearch), typeof(MushroomAttack))]
    [RequireComponent(typeof(MushroomCheckATK), typeof(MushroomHit))]
    public class Mushroom : Monster
    {
        [SerializeField] private MushroomMovement mushroomMovement;
        [SerializeField] private MushroomSearch mushroomSearch;
        [SerializeField] private MushroomAttack mushroomAttack;
        [SerializeField] private MushroomCheckATK mushroomCheckATK;
        [SerializeField] private MushroomHit mushroomHit;

        private Collider2D attackPoint;

        private void Awake()
        {
            mushroomMovement = GetComponent<MushroomMovement>();
            mushroomSearch = GetComponent<MushroomSearch>();
            mushroomAttack = GetComponent<MushroomAttack>();

            pos = GetComponent<Transform>();


            mushroomSearch.PlayerFound += OnPlayerFound;
            mushroomSearch.PlayerUnfound += OnPlayerUnfound;
        }

        private void Start()
        {
            Init();
        }
        protected override void Init()
        {
            mushroomCheckATK = GetComponentInChildren<MushroomCheckATK>();
            mushroomCheckATK.PlayerAttacked += OnPlayerAttack;
            mushroomCheckATK.PlayerUnAttacked += OnPlayerUnAttack;

            MonsterState = State.Idle;
            maxHealth = 100.0f;
            moveSpeed = 2.0f;
            attackRange = 0.3f;
            attackSpeed = 1.0f;
            attackPower = 10.0f;

            mushroomMovement.Speed = moveSpeed;
        }
        private void Update()
        {

            switch (MonsterState)
            {
                case State.Idle:
                    mushroomSearch.Search();
                    break;
                case State.Patrol:
                    mushroomSearch.Search();
                    mushroomMovement.Patrol();
                    break;
                case State.Chase:
                    mushroomSearch.Search();
                    mushroomMovement.Chase();
                    break;
                case State.Attack:
                    StartCoroutine(mushroomAttack.AttackCoroutine());
                    break;
                case State.Hit:
                    mushroomSearch.Search();

                    break;
                case State.Die:
                    break;
            }

        }

        private void OnPlayerFound()
        {
            this.target = mushroomSearch.Target;
            mushroomMovement.Target = target;
            MonsterState = State.Chase;
        }
        private void OnPlayerUnfound()
        {
            this.target = null;
            mushroomMovement.Target = null;
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