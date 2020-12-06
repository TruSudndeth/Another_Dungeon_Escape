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
        if (Input.GetKeyDown(KeyCode.Space) && Player_Input.y == 0)
        {
            Player_Input.y = jumpForce;
        }
        else if (Player_Input.y > 0)
        {
            Player_Input.y += rb_DeltaY_Velocity + Character_RB.velocity.y;
            rb_DeltaY_Velocity = Mathf.Abs(Character_RB.velocity.y);

            if(Player_Input.y <= 0)
            {
                rb_DeltaY_Velocity = 0;
                Player_Input.y = 0;
            }
        }
        GroundChecker();
        ApplyMovment = Player_Input;
        ApplyMovment.x *= Speed;
    }

    private void GroundChecker()
    {
        if(Character_RB.velocity.y < -0.1f) // needs a range for uneven ground
        {
            RaycastHit2D hit2D;
            if(Character_RB.velocity.y < 0)
            {
                if(Grounded) Grounded = false;
                Vector2 CharacterColliderOffset = transform.position;
                CharacterColliderOffset.y -= 0.5f;
                hit2D = Physics2D.Raycast(CharacterColliderOffset, Vector3.down, NextFramePosition(), layerMasking);
                Debug.DrawRay(CharacterColliderOffset,Vector2.down * NextFramePosition(), Color.green); // Delete
                if(hit2D.collider != null)
                {
                    PlayerHight_GroundCheck = hit2D.distance;
                    if(NextFramePosition() >= PlayerHight_GroundCheck)
                    {
                        Grounded = true;
                        Character_RB.velocity = Vector2.zero;
                        transform.position = hit2D.point + new Vector2(0, Collider2D_Offset.y/2);
                    }
                }
            }
        }
    }

    private float NextFramePosition()
    {
        return Mathf.Abs(Character_RB.velocity.y) * Time.fixedDeltaTime;
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
