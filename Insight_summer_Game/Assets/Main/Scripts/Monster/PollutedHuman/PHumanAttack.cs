using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.PollutedHuman
{
    public class PHumanAttack : MonsterAttack
    {
        private void Awake()
        {
            anim = GetComponent<Animator>();
        }
    }

}

