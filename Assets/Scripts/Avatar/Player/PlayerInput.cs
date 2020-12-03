using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : CharacterPhysics
{
    [SerializeField]
    private float Speed = 1;
    [SerializeField]
    private float jumpForce = 10;
    private float rb_DeltaY_Velocity = 0;
    private Vector2 Player_Input = new Vector2();

    protected override void InputMovement()
    {
        Player_Input.x = Input.GetAxis("Horizontal") * Speed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Break();
            Player_Input.y = jumpForce;
        }
        else if (Player_Input.y > 0)
        {
            Player_Input.y += rb_DeltaY_Velocity + Character_RB.velocity.y;
            rb_DeltaY_Velocity = Mathf.Abs(Character_RB.velocity.y);

            if(Player_Input.y < 0)
            {
                rb_DeltaY_Velocity = 0;
                Player_Input.y = 0;
            }
        }
        ApplyMovment = Player_Input;
    }
}
