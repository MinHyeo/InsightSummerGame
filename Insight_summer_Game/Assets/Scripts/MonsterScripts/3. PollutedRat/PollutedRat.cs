using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollutedRat : Monster
{
    void Start()
    {

    }

    public override void Attack()
    {
        //Monster �ڵ� ������
        base.Attack();
        Debug.Log("PollutedRat is Attacking");
        return;
    }
    public override void Hit()
    {
        base.Hit();
        Debug.Log("PollutedRat Hitted");
    }


    public override void Contact()
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
}
