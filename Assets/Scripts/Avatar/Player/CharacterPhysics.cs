using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CharacterPhysics : MonoBehaviour
{
    //ToDo's
    // convert line 37 to Velcoity move instead of moving with rigigbody.position
    //Player 2D collider at high speeds Clipping (Y Velocity 60) must Fix
    // Floor Composit collider is uneven and moves my character sround with zero input. (Must Fix)

    protected Rigidbody2D Character_RB;
    protected Vector2 ApplyMovment = new Vector2();
    protected CapsuleCollider2D _collider2D;

    private void OnEnable()
    {
        _collider2D = GetComponent<CapsuleCollider2D>();
        if (!_collider2D) Debug.Log("Collider 2D Required on " + gameObject.name);
        Character_RB = GetComponent<Rigidbody2D>();
        if (!Character_RB) Debug.Log("RigidBody2D Required on" + gameObject.name);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)) // Delete Line and Code Block
        {
            Debug.Break();
            Character_RB.velocity = new Vector2(0, -1000000f); //Testing High Speed Velocity Ground Clipping
        }
        InputMovement();
    }

    private void FixedUpdate()
    {
        Character_RB.velocity += ApplyMovment * Time.fixedDeltaTime;
    }

    protected virtual void InputMovement()
    {

    }
}
