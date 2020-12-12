using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Enemy : MonoBehaviour
{
    //ToDo's
    //
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

    private Animator Anim;
    private Rigidbody2D RB;
    private SpriteRenderer spriteRend;
    private bool isDead = false;
    private bool gameOver = false;
    private bool IsMoving = false;
    private Vector2 ApplyAiMove;
    void Awake()
    {
        Anim = GetComponentInChildren<Animator>();
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        RB = GetComponent<Rigidbody2D>();
        if (PointA2B != null) PointA2B.FarthestPoint(transform);
        StartCoroutine(RandomScout());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage();
        }
        CalcMoves();
    }

    private void CalcMoves()
    {
        if (PointA2B != null && IsMoving)
        {
            ApplyAiMove = PointA2B.MoveToPoint(transform.position);
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
                StartCoroutine(IsDead());
            }
        }
        ApplyAiMove.x *= Speed;
        FlipX();
        RB.velocity = new Vector2(ApplyAiMove.x, RB.velocity.y);
    }
    public virtual void TakeDamage()
    {
        if (health <= 0 && !gameOver)
        {
            gameOver = true;
            IsMoving = false;
            Anim.SetBool("IsDead", true);
            isDead = true;
        }
    }

    private IEnumerator IsDead()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }

    private IEnumerator RandomScout()
    {
        while (!isDead)
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
        }
        if (ApplyAiMove.x < 0)
        {
            if (!spriteRend.flipX) spriteRend.flipX = true;
        }
    }
}
