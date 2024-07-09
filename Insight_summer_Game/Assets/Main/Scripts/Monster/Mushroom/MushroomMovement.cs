using System;
using UnityEngine;
using UnityEngine.U2D;

namespace Monster.Mushroom
{
    public class MushroomMovement : MonsterMovement
    {
        public override void Patrol()
        {
            sprite.flipX = rigid.velocity.x > 0f ? false : true;
            rigid.velocity = new Vector2(nextDir * Speed, rigid.velocity.y);

        }
        public override void Chase()
        {
            if (Target != null)
            {
                dir = Target.position.x < transform.position.x ? -1 : 1;
                sprite.flipX = rigid.velocity.x > 0f ? false : true;
                rigid.velocity = new Vector2(dir * Speed, rigid.velocity.y);
            }

        }
    }
}