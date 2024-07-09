using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PollutedHuman : Monster
{
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

        //회전
        if ((dirVec.x > 0 && transform.localScale.x > 0) || (dirVec.x < 0 && transform.localScale.x < 0)) {
            transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
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

    //몸빵 충돌 구현하기
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        Contact();
    }*/
}