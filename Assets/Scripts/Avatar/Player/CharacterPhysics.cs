using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPhysics : MonoBehaviour
{
    //ToDo's
    //Player 2D collider at high speeds Clipping (Y Velocity 60) must Fix
    protected Rigidbody2D Character_RB;
    protected Vector2 ApplyMovment = new Vector2();

    private void OnEnable()
    {
        Character_RB = GetComponent<Rigidbody2D>();
        if (!Character_RB) Debug.Log("RigidBody2D Required on" + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        InputMovement();
    }

    private void FixedUpdate()
    {
        Character_RB.position += ApplyMovment * Time.deltaTime;
    }

    protected virtual void InputMovement()
    {

    }
}
