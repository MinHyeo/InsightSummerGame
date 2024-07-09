using Monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    protected Rigidbody2D rigid;
    protected SpriteRenderer sprite;
    protected Animator animator;
    protected Transform pos;
    protected MonsterHit hit;
    public Transform Target { protected get; set; }
    public float Speed { protected get; set; }

    protected int nextDir;
    protected int changeTime;
    protected int dir;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        pos = GetComponent<Transform>();
    }
    private void Start()
    {
        ChangeDirection();
    }


    public virtual void Patrol()
    {
        sprite.flipX = rigid.velocity.x > 0f ? true : false;
        rigid.velocity = new Vector2(nextDir * Speed, rigid.velocity.y);

    }
    public virtual void Chase()
    {
        if (Target != null)
        {
            dir = Target.position.x < transform.position.x ? -1 : 1;
            sprite.flipX = rigid.velocity.x > 0f ? true : false;
            rigid.velocity = new Vector2(dir * Speed, rigid.velocity.y);
        }

    }
    public virtual void ChangeDirection()
    {
        changeTime = UnityEngine.Random.Range(1, 4);
        nextDir = Normalize(UnityEngine.Random.Range(-5, 5));
        Invoke("ChangeDirection", changeTime);
    }
    private int Normalize(float value)
    {
        if (value < -1.67f) return -1;
        else if (value > 1.67f) return 1;
        else return 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Barrier"))
        {
            nextDir *= -1;
        }
    }

}
