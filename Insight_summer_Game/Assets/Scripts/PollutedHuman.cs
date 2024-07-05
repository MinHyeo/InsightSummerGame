using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollutedHuman : Monster
{
    [Header("Monster Component")]
    public Animator monsterAnimator;

    // Start is called before the first frame update
    void Start()
    {
        monsterAnimator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void Attack()
    {
        return;
    }

    public override void Chase()
    {
        return;
    }

    public override void Contact()
    {
        return;
    }

    public override void Dead()
    {
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
}