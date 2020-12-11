using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moss_Giant : Enemy, IEnemy
{
    [SerializeField]
    private MoveAnB PointA2B;
    private Vector2 ApplyAIMove = Vector2.zero;
    private Rigidbody2D RB;
    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        if (PointA2B != null) PointA2B.FarthestPoint(transform);
    }
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        CalcMoves();
    }

    public void FixedUpdate()
    {
        ApplyAIMove.x *= Speed;
        RB.velocity = new Vector2(ApplyAIMove.x, RB.velocity.y);
    }

    private void CalcMoves()
    {
        if(PointA2B != null)
        {
            ApplyAIMove = PointA2B.MoveToPoint(transform.position);
        }
        else
            ApplyAIMove = Vector2.zero;
        
    }

    public void TakeDamage()
    {
        health--;
        if(health <= 0)
        {
            Debug.Log("Enemy Killed");
        }
    }
}
