using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.Mushroom
{
    public class MushroomAttack : MonsterAttack
    {
        private void Awake()
        {
            anim = GetComponent<Animator>();
        }
    }
}