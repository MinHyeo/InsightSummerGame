using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollutedMushroom : Monster
{
    void Start()
    {

    }

    public override void Attack()
    {
        //Monster ÄÚµå °¡Á®¿È
        base.Attack();
        Debug.Log("PollutedMushroom is Attacking");
        return;
    }

    public override void Hit()
    {
        base.Hit();
        monsterAnimator.SetTrigger("Hit");
        monsterAnimator.SetBool("IsDetacted", false);
        Debug.Log("PollutedMushroom Hitted");
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
