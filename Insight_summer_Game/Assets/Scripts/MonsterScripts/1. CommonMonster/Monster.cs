using System;
using UnityEngine;

//Monster Abstract class
public abstract class Monster : MonoBehaviour
{
    //These veriables are initialized at inspector
    [Header("Monster Status")]
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float speed;
    [SerializeField] protected float attackPower;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float searchRange;

    [Header("Monster Component")]
    [SerializeField] protected Animator monsterAnimator;
    [SerializeField] protected Collider2D monsterColl;
    [SerializeField] protected Rigidbody2D monsterRigid;
    [SerializeField] protected SpriteRenderer monsterSpriteRenderer;

    [Header("Target Player Info")]
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected Transform targetPlayer;

    //Monster Behaviors(Method)
    public abstract void Contact();
    public abstract void Idle(); //Animation
    public abstract void Walk(); //Animation
    private void Update()
    {
        Search();
    }

    private void Awake()
    {
        monsterAnimator = GetComponent<Animator>();
        monsterColl = GetComponent<Collider2D>();
        monsterRigid = GetComponent<Rigidbody2D>();
        monsterSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    public virtual void Hit()
    {
        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        monsterAnimator.SetBool("Dead", true);
    }

    public virtual void Attack()
    {
        targetPlayer.GetComponent<HeroKnight>().Hit(attackPower);
    }

    public void Chase() {
        Debug.Log("Monster is Chasing");
        monsterAnimator.SetBool("IsDetacted", true);
        //���� ����  
        Vector2 dirVec = targetPlayer.position - transform.position;

        //ȸ��
        if ((dirVec.x > 0 && transform.localScale.x > 0) || (dirVec.x < 0 && transform.localScale.x < 0))
        {
            transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        //�Ÿ� ����
        Vector2 nextVec = dirVec.normalized * (speed * 1.3f) * Time.deltaTime;
        nextVec.y = 0;

        //�̵�
        monsterRigid.MovePosition(monsterRigid.position + nextVec);
        monsterRigid.velocity = Vector2.zero;
        return;
    }
    public void Search() {
        //Ž�� ���� ����
        targetPlayer = (Physics2D.CircleCast(transform.position, searchRange, Vector2.zero, 0, playerLayer)).transform;
        //�÷��̾ ã���� ���
        if (targetPlayer != null)
        {
            Debug.Log("Player Detacted!!");
            if (Vector2.Distance(transform.position, targetPlayer.position) <= attackRange)
            {
                Attack();
            }
            Chase();
        }
        else
        {
            monsterAnimator.SetBool("IsDetacted", false);
        }
    }

    //���� Ȯ�ο�
    void OnDrawGizmosSelected()
    {
        //���� ����
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, searchRange);
        //���� ����
        Gizmos.color = Color.red;
        Vector3 attackDistance = new Vector3(transform.position.x - attackRange, transform.position.y, transform.position.z);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
