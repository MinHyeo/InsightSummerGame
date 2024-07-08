using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("")]
    public float health;
    private void Awake()
    {
        health = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(float Damage = 0)
    {
        Debug.Log("Player Hitted " + Damage);
        health -= Damage;
    }

}
