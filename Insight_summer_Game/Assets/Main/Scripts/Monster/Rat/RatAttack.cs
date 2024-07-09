using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.Rat
{
    public class RatAttack : MonsterAttack
    {
        private void Awake()
        {
            anim = GetComponent<Animator>();
        }
    }
}