using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneSpider : Monster
{
    void Start()
    {

    }

    public override void Attack()
    {
        //Monster �ڵ� ������
        base.Attack();
        Debug.Log("BoneSpider is Attacking");
        return;
    }

    public override void Contact()
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
}
