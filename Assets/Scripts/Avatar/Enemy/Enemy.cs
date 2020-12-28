using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Enemy : MonoBehaviour
{
    //ToDo's
    // Enemy moves while Dead Must Fix
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    protected float Speed = 1;
    [SerializeField]
    protected int health = 100;
    [SerializeField]
    protected int Gems = 0;
    [SerializeField]
    private MoveAnB PointA2B;
    [SerializeField]
    private Vector2 IdleMinMaxTime;
    [SerializeField]
    private Vector2 WalkMinMaxTime;
    [SerializeField]
    private Vector3 EnemyRaycastOffset;

    private BoxCollider2D boxCollider2D;

    private GameObject EnemyPlayer;
    protected Animator Anim;
    private Rigidbody2D RB;
    private SpriteRenderer spriteRend;
    protected bool isDead = false;
    protected bool gameOver = false;
    protected bool IsMoving = false;
    protected bool InCombat = false;
    private Vector2 ApplyAiMove;
    private Vector2 LookDirection;
    [SerializeField]
    protected float attackDistance = 0;
    [SerializeField]
    private float attackRange = 0;
    private float playerDistance = 0;
    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        Anim = GetComponentInChildren<Animator>();
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        RB = GetComponent<Rigidbody2D>();
        if (PointA2B != null) PointA2B.FarthestPoint(transform);
        StartCoroutine(RandomScout());
    }
    private void Update()
    {
        CalcMoves();
    }

    private void CalcMoves()
    {
        if (PointA2B != null && IsMoving && !gameOver)
        {
            ApplyAiMove = PointA2B.MoveToPoint(transform.position);
        }
        else if (EnemyPlayer && playerDistance > boxCollider2D.size.x/2 + attackRange)
        {
            ApplyAiMove = PointA2B.MoveToPlayer(EnemyPlayer.transform.position);
        }
        else
            ApplyAiMove = Vector2.zero;
    }

    public void FixedUpdate()
    {
        if (isDead)
        {
            if(IsMoving) IsMoving = false;
            if (!Anim.GetBool("IsDead"))
            {
                isDead = false;
                gameOver = true;
                StartCoroutine(IsDead());
            }
        }
        CheckForCombat();
        if (InCombat)
        {
            IsMoving = false;
            if (!Anim.GetBool("Attack"))
            {
                if (playerDistance <= attackDistance)
                {

                    Anim.SetBool("Attack", true);
                }
            }
        }
        else
        {
            if (!Anim.GetBool("Attack")) IsMoving = true;
        }
        ApplyAiMove.x *= Speed;
        FlipX();
        RB.velocity = new Vector2(ApplyAiMove.x, RB.velocity.y);
    }

    private IEnumerator TriggerRandomAttack()
    {
        float _attackDelay = Random.Range(0, 0.1f);
        yield return new WaitForSeconds(_attackDelay);
        if (playerDistance <= attackDistance && playerDistance != 0)
            Anim.SetBool("Attack", true);
    }

    private void CheckForCombat()
    {
        RaycastHit2D hit2D;
        hit2D = Physics2D.Raycast(transform.position - EnemyRaycastOffset, LookDirection, 3, layerMask);
        Debug.DrawRay(transform.position - EnemyRaycastOffset, LookDirection * 3, Color.yellow);
        if(hit2D && hit2D.collider.tag == "Player")
        {
            if(!EnemyPlayer)EnemyPlayer = hit2D.transform.gameObject;
            playerDistance = hit2D.distance;
            InCombat = true;
            return;
        }else
        {
            if (EnemyPlayer) EnemyPlayer = null;
            playerDistance = 0;
            InCombat = false;
        }
    }

    private IEnumerator IsDead()
    {

        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    private IEnumerator RandomScout()
    {
        while (!gameOver)
        {
            IsMoving = false;
            Anim.SetBool("Walking", false);
            yield return new WaitForSeconds(Random.Range(IdleMinMaxTime.x, IdleMinMaxTime.y));
            IsMoving = true;
            Anim.SetBool("Walking", true);
            yield return new WaitForSeconds(Random.Range(WalkMinMaxTime.x, WalkMinMaxTime.y));
        }
    }
    private void FlipX()
    {
        if (ApplyAiMove.x > 0)
        {
            if (spriteRend.flipX) spriteRend.flipX = false;
            if (spriteRend.transform.localPosition.x < 0)
            {
                spriteRend.transform.localPosition *= new Vector2(-1, 1);
                LookDirection = Vector2.right;
            }
        }
        if (ApplyAiMove.x < 0)
        {
            if (spriteRend.transform.localPosition.x > 0)
            {
                spriteRend.transform.localPosition *= new Vector2(-1, 1);
                LookDirection = Vector2.left;
            }
            if (!spriteRend.flipX) spriteRend.flipX = true;
        }
    }
}
