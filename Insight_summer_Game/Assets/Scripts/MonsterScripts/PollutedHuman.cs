using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PollutedHuman : Monster
{

    private void Awake()
    {
        monsterAnimator = GetComponent<Animator>();
        monsterColl = GetComponent<Collider2D>();
        monsterRigid = GetComponent<Rigidbody2D>();
        monsterSpriteRenderer = GetComponent<SpriteRenderer>(); 
    }
    void Start()
    {
        
    }
    void Update()
    {
        Search();
    }
    public override void Attack()
    {
        Debug.Log("Monster is Attacking");

        targetPlayer.GetComponent<HeroKnight>().Hit(attackPower);
        return;
    }

    public override void Chase()
    {
        Debug.Log("Monster is Chasing");
        monsterAnimator.SetBool("IsDetacted", true);
        //방향 측정  
        Vector2 dirVec = targetPlayer.position -  transform.position;  
        if (dirVec.x > 0) { 
            monsterSpriteRenderer.flipX = true;
        }
        else
        {
            monsterSpriteRenderer.flipX = false;
        }
        //거리 설정
        Vector2 nextVec = dirVec.normalized * (speed * 1.3f) * Time.deltaTime;  
        nextVec.y = 0;
        //이동
        monsterRigid.MovePosition(monsterRigid.position + nextVec);   
        monsterRigid.velocity = Vector2.zero;
        return;
    }

    public override void Contact()
    {
        Debug.Log("Player Hitted");
        targetPlayer.GetComponent<HeroKnight>().Hit(1.0f);
        return;
    }

    public override void Dead()
    {
        monsterAnimator.SetBool("Dead", true);
        return;
    }

    public override void Hit()
    {
        Debug.Log("Monster Hitted");
        if (currentHealth <= 0)
        {
            Dead();
        }
        return;
    }

    public override void Idle()
    {
        Debug.Log("Monster is Idle Mode now");
        return;
    }

    public override void Walk()
    {
        Debug.Log("Monster is Scouting");
        return;
    }

    public override void Search()
    {
        //탐지 범위 생성
        targetPlayer = (Physics2D.CircleCast(transform.position, searchRange, Vector2.zero, 0, playerLayer)).transform;
        //플레이어를 찾았을 경우
        if (targetPlayer != null) {
            Debug.Log("Player Detacted!!");
            if (Vector2.Distance(transform.position, targetPlayer.position) <= attackRange) {
                Attack();
            }
            Chase();
        }
        else
        {
            monsterAnimator.SetBool("IsDetacted", false);
        }
    }
    //몸빵 충돌 구현하기
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        Contact();
    }*/
    void OnDrawGizmosSelected()
    {
        //추적 범위
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, searchRange);
        //공격 범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}