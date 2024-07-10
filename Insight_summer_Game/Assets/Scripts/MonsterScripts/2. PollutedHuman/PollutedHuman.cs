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
    public override void Attack()
    {
        //Monster ÄÚµå °¡Á®¿È
        base.Attack();
        Debug.Log("PollutedHuman is Attacking");
        return;
    }
    public override void Hit()
    {
        base.Hit();
        Debug.Log("PollutedHuman Hitted");
    }

    public override void Contact()
    {
        Debug.Log("Player Hitted");
        targetPlayer.GetComponent<HeroKnight>().Hit(1.0f);
        return;
    }

  
    public override void Idle()
    {
        Debug.Log("PollutedHuman is Idle Mode now");
        return;
    }

    public override void Walk()
    {
        Debug.Log("PollutedHuman is Scouting");
        return;
    }

    //¸ö»§ Ãæµ¹ ±¸ÇöÇÏ±â
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        Contact();
    }*/
}