using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace Monster.Slime
{
    public class SlimeAttack : MonsterAttack
    {
        private void Awake()
        {
            anim = GetComponent<Animator>();
        }
    }
}