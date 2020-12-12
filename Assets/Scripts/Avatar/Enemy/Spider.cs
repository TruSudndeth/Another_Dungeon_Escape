using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy , IEnemy
{
    private Rigidbody2D RB;
    private Vector2 ApplyMove;
    [SerializeField]
    private MoveAnB PointA2B;
    private SpriteRenderer spriteRend;
    private Animator Anim;
    private bool isWalking = false;
    private bool IsDead = false;

    void Awake()
    {
        Anim = GetComponentInChildren<Animator>();
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        RB = GetComponent<Rigidbody2D>();
        if (PointA2B != null) PointA2B.FarthestPoint(transform);
        StartCoroutine(WalkandIdle());
    }
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage()
    {
        health -= 50;
        if(health <= 0)
        {
            Debug.Log("Spider Dead");
        }
    }

    public override void Update()
    {
        CalcMove();
    }

    private void FixedUpdate()
    {
        ApplyMove.x *= Speed;
        FlipX();
        RB.velocity = new Vector2(ApplyMove.x, RB.velocity.y);
    }

    private void CalcMove()
    {
        if(PointA2B != null && isWalking)
        {
            ApplyMove = PointA2B.MoveToPoint(transform.position);
        }
        else
        {
            ApplyMove = Vector2.zero;
        }
    }

    private void FlipX()
    {
        if(ApplyMove.x > 0 && spriteRend.flipX)
        {
            spriteRend.flipX = false;
        }
        if(ApplyMove.x < 0 && !spriteRend)
        {
            spriteRend.flipX = true;
        }
    }

    private IEnumerator WalkandIdle()
    {
        while(!IsDead)
        {
            Anim.SetBool("Walking", false);
            isWalking = false;
            yield return new WaitForSeconds(Random.Range(2.5f, 5f));
            Anim.SetBool("Walking", true);
            isWalking = true;
            yield return new WaitForSeconds(Random.Range(1f, 2.5f));
        }
    }
}
