using UnityEngine;
using System.Collections;

namespace Monster.PollutedHuman
{
    [RequireComponent(typeof(PHumanMovement), typeof(PHumanSearch), typeof(PHumanAttack))]
    [RequireComponent(typeof(PHumanCheckATK), typeof(PHumanHit))]
    public class PollutedHuman : Monster
    {
        [SerializeField] private PHumanMovement pHumanMovement;
        [SerializeField] private PHumanSearch pHumanSearch;
        [SerializeField] private PHumanAttack pHumanAttack;
        [SerializeField] private PHumanCheckATK pHumanCheckATK;
        [SerializeField] private PHumanHit pHumanHit;

        private Collider2D attackPoint;

        private void Awake()
        {
            pHumanMovement = GetComponent<PHumanMovement>();
            pHumanSearch = GetComponent<PHumanSearch>();
            pHumanAttack = GetComponent<PHumanAttack>();

            pos = GetComponent<Transform>();


            pHumanSearch.PlayerFound += OnPlayerFound;
            pHumanSearch.PlayerUnfound += OnPlayerUnfound;
        }


        private void Start()
        {
            Init();
        }
        protected override void Init()
        {
            pHumanCheckATK = GetComponentInChildren<PHumanCheckATK>();
            pHumanCheckATK.PlayerAttacked += OnPlayerAttack;
            pHumanCheckATK.PlayerUnAttacked += OnPlayerUnAttack;

            MonsterState = State.Idle;
            maxHealth = 100.0f;
            moveSpeed = 1.8f;
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
                    pHumanSearch.Search();
                    break;
                case State.Patrol:
                    pHumanSearch.Search();
                    pHumanMovement.Patrol();
                    break;
                case State.Chase:
                    pHumanSearch.Search();
                    pHumanMovement.Chase();
                    break;
                case State.Attack:
                    StartCoroutine(pHumanAttack.AttackCoroutine());
                    break;
                case State.Die:
                    break;
            }

        }

        private void OnPlayerFound()
        {
            this.target = pHumanSearch.Target;
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