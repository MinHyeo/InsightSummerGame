using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHuman : Monster
{
    void Start()
    {

    }
    public override void Attack()
    {
        //Monster ÄÚµå °¡Á®¿È
        base.Attack();
        Debug.Log("SlimeHuman is Attacking");
        return;
    }
    public override void Hit()
    {
        base.Hit();
        Debug.Log("SlimeHuman Hitted");
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
