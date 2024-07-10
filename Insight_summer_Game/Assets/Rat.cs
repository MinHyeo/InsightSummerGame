using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rat : Monster
{
    HeroKnight player;

    protected override void Awake()
    {
        base.Awake();
        attackPower = 10f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Move()
    {
        base.Move();
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {


            Attack(target);
    }

    private void Attack(Transform target)
    {
         target.GetComponent<HeroKnight>();
        if (player != null)
        {

            player.Hit();
        }
    }

    
}


