using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollutedRat : Monster
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
        throw new System.NotImplementedException();
    }

    public override void Chase()
    {
        throw new System.NotImplementedException();
    }

    public override void Contact()
    {
        throw new System.NotImplementedException();
    }

    public override void Dead()
    {
        throw new System.NotImplementedException();
    }

    public override void Hit()
    {
        throw new System.NotImplementedException();
    }

    public override void Idle()
    {
        throw new System.NotImplementedException();
    }

    public override void Walk()
    {
        throw new System.NotImplementedException();
    }

    public override void Search()
    {
        throw new System.NotImplementedException();
    }
}
