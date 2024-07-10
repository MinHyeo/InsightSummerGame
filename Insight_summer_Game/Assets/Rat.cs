using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Monster
{


    protected override void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    protected override void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        rigid.velocity = new Vector2(nextMove * movePower, rigid.velocity.y);
        CheckGround();

        nextMove = -1;

    }
}

