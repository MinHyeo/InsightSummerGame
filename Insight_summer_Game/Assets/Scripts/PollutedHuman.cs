using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PollutedHuman : Monster
{
    [Header("Monster Component")]
    public Animator monsterAnimator;
    public Collider2D monsterColl;
    public Rigidbody2D monsterRigid;
    public SpriteRenderer monsterSpriteRenderer;

    [Header("Target Player Info")]
    public LayerMask playerLayer;
    public Transform targetPlayer;

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
        Scan();
        if (IsAttackableDistance()) {
            Attack();
        }
        if (currentHealth <= 0) { 
            Dead();
        }
    }
    public override void Attack()
    {
        monsterAnimator.SetTrigger("Attack");
        return;
    }

    public override void Chase()
    {
        monsterAnimator.SetBool("IsDetacted", true);
        //�����̱�
        Vector2 dirVec = targetPlayer.position -  transform.position;  //���� ����  
        if (dirVec.x > 0) { 
            monsterSpriteRenderer.flipX = true;
        }
        else
        {
            monsterSpriteRenderer.flipX = false;
        }
        Vector2 nextVec = dirVec.normalized * speed * Time.deltaTime;  //�Ÿ� ����
        nextVec.y = 0;
        monsterRigid.MovePosition(monsterRigid.position + nextVec);   //�̵�
        monsterRigid.velocity = Vector2.zero;
        return;
    }

    public override void Contact()
    {
        return;
    }

    public override void Dead()
    {
        monsterAnimator.SetBool("Dead", true);
        return;
    }

    public override void Hit()
    {
        return;
    }

    public override void Idle()
    {
        return;
    }

    public override void Walk()
    {
        return;
    }

    public override void Scan()
    {
        //Ž�� ���� ����
        targetPlayer = (Physics2D.CircleCast(transform.position, searchRange, Vector2.zero, 0, playerLayer)).transform;
        //�÷��̾ ã���� ���
        if (targetPlayer != null) {
            Debug.Log("Player Detacted!!");
            Chase();
        }
        else
        {
            monsterAnimator.SetBool("IsDetacted", false);
        }
    }

    public bool IsAttackableDistance() {
        //���� ������ �Ÿ����� ����
        if (targetPlayer == null) {
            return false;
        }
        float distance = Vector2.Distance(transform.position, targetPlayer.position);
        if (distance <= attackRange){
            Debug.Log("Monster Attackable Range");
            return true;   
        }
        else
        {
            return false;
        }
    }
}