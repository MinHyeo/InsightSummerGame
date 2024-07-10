using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Monster : MonoBehaviour
{
    public float health;
    public float attackPower;
    public float attackRange;
    public float searchRange;
    public float attackSpeed;
    public float movePower = 2f;

    public Transform target;
    public float detectionRadius = 3f;
    public float detectionAngle = 180f;
    public float chaseTime;
    public float chaseDuration = 3f;

    protected Rigidbody2D rigid;
    protected int nextMove;
    protected bool isChasing = false;
    protected bool facingRight = true;



    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 5);
    }

    protected virtual void Start()
    {
    }

    protected virtual void FixedUpdate()
    {
        if (isChasing)
        {
            Chase();
        }
        else
        {
            Search();
        }
    }

    protected virtual void Move()
    {
        rigid.velocity = new Vector2(nextMove * movePower, rigid.velocity.y);
        CheckGround();

        if (nextMove == -1 && facingRight)
        {
            Flip();
        }
        else if (nextMove == 1&& !facingRight)
        {
            Flip();
        }

    }

    protected virtual void Think()
    {
        nextMove = Random.Range(-1, 2);
        Invoke("Think", 5);
    }

    protected virtual void CheckGround()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));

        if (rayHit.collider == null)
        {
            nextMove *= -1;
            CancelInvoke();
            Invoke("Think", 5);
        }

    }
    protected virtual void Search()
    {
        if (Detect(target))
        {
            isChasing = true;
            chaseTime = chaseDuration;
        }
        else
        {
            Move();
        }
    }

    protected virtual void Chase()
    {
        if (target == null) return;

        chaseTime -= Time.fixedDeltaTime;
        if(chaseTime <= 0)
        {
            isChasing=false;
            return;
        }

        Vector3 directionToTarget = (target.position - transform.position).normalized;
        rigid.velocity = new Vector2(directionToTarget.x * movePower, rigid.velocity.y);

        if ((directionToTarget.x > 0 && !facingRight) || (directionToTarget.x < 0 && facingRight))
        {
            Flip();
        }

        
    }

    protected virtual void Hit()
    {
        health -= 10;

        if (health <= 0) Death();
    }
    protected virtual void Death()
    {
       
    }



    protected virtual bool Detect(Transform target)
    {
        if (target == null) return false;

        Vector2 directionToTarget = target.position - transform.position; ///////
        float distanceToTarget = directionToTarget.magnitude;

        if (distanceToTarget <= detectionRadius)
        {
            directionToTarget.Normalize();

            float angleToTarget = Vector2.Angle(transform.right, directionToTarget);


            if (angleToTarget <= detectionAngle)
            {
                Debug.Log("Target detected within fan area: " + target.name);
                Debug.Log("Distance to target: " + distanceToTarget);
                return true;
            }
        }
        return false;
        
    }


    protected virtual void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 position = transform.position;

        float startAngle = -detectionAngle;
        float endAngle = detectionAngle;

        Vector3 startDirection = Quaternion.Euler(0, 0, startAngle) * transform.right * detectionRadius;
        Vector3 endDirection = Quaternion.Euler(0, 0, endAngle) * transform.right * detectionRadius;

        Gizmos.DrawLine(position, position + startDirection);

        Gizmos.DrawLine(position, position + endDirection);

        float angleStep = (endAngle - startAngle) / 20f;
        for (float angle = startAngle; angle < endAngle; angle += angleStep)
        {
            Vector3 from = Quaternion.Euler(0, 0, angle) * transform.right * detectionRadius;
            Vector3 to = Quaternion.Euler(0, 0, angle + angleStep) * transform.right * detectionRadius;
            Gizmos.DrawLine(position + from, position + to);
        }
    }
}
