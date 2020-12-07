using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : CharacterPhysics
{
    //ToDo's
    // RayCast A Sphere instead of a line for Rock Glitches player jumps on rocks with no input
    // a high start velocity clips through ground before first frame (Look up the order of execution)
    [SerializeField]
    private float jumpForce = 10;
    [SerializeField]
    private LayerMask layerMasking;
    private float rb_DeltaY_Velocity = 0;
    private Vector2 Collider2D_Offset;
    public bool Jumped = false;
    [SerializeField]
    private float PlayerHight_GroundCheck = 0;
    [SerializeField]
    private float Speed;

    [SerializeField]
    public Vector2 Player_Input = new Vector2();
    public bool Grounded = false;
    private void Start()
    {
        Collider2D_Offset = _collider2D.size;
        if(Collider2D_Offset == Vector2.zero)
        {
            StartCoroutine(ColliderCheck());
        }
    }

    protected override void InputMovement()
    {
        Player_Input.x = Input.GetAxis("Horizontal");
        if ((Input.GetKeyDown(KeyCode.Space)) && Grounded)
        {
            Character_RB.velocity = new Vector2(Character_RB.velocity.x, 0);
            Character_RB.velocity += new Vector2(0, jumpForce);
            Jumped = true;
            Grounded = false;
        }
        GroundChecker();
        ApplyMovment = Player_Input;
        ApplyMovment.x *= Speed;

    }

    private void GroundChecker()
    {
        if(Character_RB.velocity.y < -0.5f || Character_RB.velocity.y > 0.5f) // needs a range for uneven ground
        {
            if(Character_RB.velocity.y < 0)
            {
                Grounded = false;
                DynamicVelocity(-1, Vector2.down);
            }
            if(Character_RB.velocity.y > 0)
            {
                DynamicVelocity(1, Vector2.up);
            }
        }
    }

    private void DynamicVelocity(int Y_DirectionStartOffset, Vector2 CastDirection)
    {
        RaycastHit2D hit2D;
        Vector2 CharacterColliderOffset = transform.position;
        CharacterColliderOffset.y += Collider2D() * Y_DirectionStartOffset;
        hit2D = Physics2D.Raycast(CharacterColliderOffset, CastDirection, NextFramePosition(), layerMasking);
        Debug.DrawRay(CharacterColliderOffset, CastDirection * NextFramePosition(), Color.green); // Delete
        if (hit2D.collider != null)
        {
            PlayerHight_GroundCheck = hit2D.distance;
            if (NextFramePosition() >= PlayerHight_GroundCheck)
            {
                if(Y_DirectionStartOffset < 0)
                {
                    Grounded = true;
                    Jumped = false;
                }
                Character_RB.velocity = Vector2.zero;
                transform.position = hit2D.point + new Vector2(0, Collider2D_Offset.y / 2 * -Y_DirectionStartOffset);
            }
        }
    }

    private float NextFramePosition()
    {
        return Mathf.Abs(Character_RB.velocity.y) * Time.fixedDeltaTime;
    }

    private float Collider2D()
    {
       return _collider2D.size.y / 2;
    }

    private IEnumerator ColliderCheck()
    {
        yield return new WaitForEndOfFrame();
        if(Collider2D_Offset == Vector2.zero)
        {
            Debug.Log("Coroutine Find Collider Failed" + transform.name);
        }
    }
}
