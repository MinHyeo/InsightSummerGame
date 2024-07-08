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
    public float movePower = 1f;

    public Transform target;
    public float detectionRadius = 3f;
    public float detectionAngle = 45f;

    private Rigidbody2D rigid;
    private Vector3 movement;
    private int movementFlag = 0;
    private bool isChasing = false;
    private bool isDetectingWall = false;
    private float idleTime = 2f;
    private float moveDuration = 3f;
    private bool facingRight = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine("ChangeMovement");
    }

    private void FixedUpdate()
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

    void Search()
    {
        Detect(target);

        if (!isDetectingWall)
        {
            Move();
        }
    }

    void Chase()
    {
        if (target == null) return;

        Vector3 directionToTarget = (target.position - transform.position).normalized;
        rigid.velocity = new Vector2(directionToTarget.x * movePower, rigid.velocity.y);

        if ((directionToTarget.x > 0 && !facingRight) || (directionToTarget.x < 0 && facingRight))
        {
            Flip();
        }
    }

    void Death()
    {
       
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (movementFlag == 1)
        {
            moveVelocity = Vector3.left;
            if (facingRight) Flip();
        }
        else if (movementFlag == 2)
        {
            moveVelocity = Vector3.right;
            if (!facingRight) Flip();
        }

        transform.position += moveVelocity * movePower * Time.fixedDeltaTime;
    }

    public void Detect(Transform target)
    {
        if (target == null) return;

        Vector2 directionToTarget = target.position - transform.position;
        float distanceToTarget = directionToTarget.magnitude;

        if (distanceToTarget <= detectionRadius)
        {
            directionToTarget.Normalize();

            float angleToTarget = Vector2.Angle(transform.right, directionToTarget);

            if (angleToTarget <= detectionAngle)
            {
                Debug.Log("Target detected within fan area: " + target.name);
                Debug.Log("Distance to target: " + distanceToTarget);
                isChasing = true;
            }
        }
    }

    IEnumerator ChangeMovement()
    {
        while (true)
        {
            movementFlag = Random.Range(0, 3);

            if (movementFlag != 0)
            {
                yield return new WaitForSeconds(moveDuration);
            }

            movementFlag = 0;
            yield return new WaitForSeconds(idleTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            movementFlag = movementFlag == 1 ? 2 : 1;
            isDetectingWall = true;
            Invoke("ResetWallDetection", 1f); 
        }
    }

    private void ResetWallDetection()
    {
        isDetectingWall = false;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnDrawGizmos()
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
