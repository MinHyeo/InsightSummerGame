using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "MonsterData")]
public class MonsterData : ScriptableObject
{
    public float health;
    public float speed;
    public float attackPower;
    public float attackDelay;
    public Vector2 attackRange;
    public Vector2 searchRange;
}
