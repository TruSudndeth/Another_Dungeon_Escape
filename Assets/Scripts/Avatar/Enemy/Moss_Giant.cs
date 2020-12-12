using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moss_Giant : Enemy, IEnemy
{
    [SerializeField]
    private MoveAnB PointA2B;
    private Animator Anim;
    private Vector2 ApplyAIMove = Vector2.zero;
    private Rigidbody2D RB;
    private bool isDead = false;
    private bool IsMoving = false;
    private SpriteRenderer SpriteRend;
    void Awake()
    {
        SpriteRend = GetComponentInChildren<SpriteRenderer>();
        Anim = GetComponentInChildren<Animator>();
        RB = GetComponent<Rigidbody2D>();
        if (PointA2B != null) PointA2B.FarthestPoint(transform);
        StartCoroutine(RandomScout());
    }
    public override void Attack()
    {
        Anim.SetBool("Attack", true);
    }

    public override void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage();
        }
        CalcMoves();
    }

    public void FixedUpdate()
    {
        if(isDead)
        {
            if (!Anim.GetBool("IsDeat"))
            {
                isDead = false;
                StartCoroutine(IsDead());
            }
        }
        ApplyAIMove.x *= Speed;
        FlipX();
        RB.velocity = new Vector2(ApplyAIMove.x, RB.velocity.y);
    }

    private void CalcMoves()
    {
        if(PointA2B != null && IsMoving)
        {
            ApplyAIMove = PointA2B.MoveToPoint(transform.position);
        }
        else
            ApplyAIMove = Vector2.zero;
    }

    public void TakeDamage()
    {
        health -= 25;
        if(health <= 0)
        {
            IsMoving = false;
            Anim.SetBool("IsDead", true);
            isDead = true;
        }
    }

    private IEnumerator IsDead()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private IEnumerator RandomScout()
    {
        while(!isDead)
        {
            IsMoving = false;
            Anim.SetBool("Walking", false);
            yield return new WaitForSeconds(Random.Range(2, 5f));
            IsMoving = true;
            Anim.SetBool("Walking", true);
            yield return new WaitForSeconds(Random.Range(5, 7));
        }
    }
    private void FlipX()
    {
        if (ApplyAIMove.x > 0)
        { 
            if (SpriteRend.flipX) SpriteRend.flipX = false;
        }
        if(ApplyAIMove.x < 0)
        {
            if (!SpriteRend.flipX) SpriteRend.flipX = true;
        }
    }
}
