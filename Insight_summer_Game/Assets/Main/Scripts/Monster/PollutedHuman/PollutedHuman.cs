using UnityEngine;
using System.Collections;

namespace Monster.PollutedHuman
{
    [RequireComponent(typeof(PHumanMovement), typeof(MonsterSearch), typeof(PHumanAttack))]
    [RequireComponent(typeof(CheckAttackable))]
    public class PollutedHuman : Monster
    {
        [SerializeField] private PHumanMovement pHumanMovement;
        [SerializeField] private MonsterSearch monsterSearch;
        [SerializeField] private PHumanAttack pHumanAttack;
        [SerializeField] private CheckAttackable checkAttackable;

        private Collider2D attackPoint;

        private void Awake()
        {
            pHumanMovement = GetComponent<PHumanMovement>();
            monsterSearch = GetComponent<MonsterSearch>();
            pHumanAttack = GetComponent<PHumanAttack>();

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
            moveSpeed = 2.0f;
            attackRange = 0.3f;
            attackSpeed = 1.0f;
            attackPower = 10.0f;

            pHumanMovement.Speed = moveSpeed;
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
                    pHumanMovement.Patrol();
                    break;
                case State.Chase:
                    monsterSearch.Search();
                    pHumanMovement.Chase();
                    break;
                case State.Attack:
                    //StartCoroutine(ratAttack.AttackCoroutine());
                    break;
                case State.Die:
                    break;
            }

        }

        private void OnPlayerFound()
        {
            this.target = monsterSearch.Target;
            pHumanMovement.Target = target;
            MonsterState = State.Chase;
        }
        private void OnPlayerUnfound()
        {
            this.target = null;
            pHumanMovement.Target = null;
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
    }
}