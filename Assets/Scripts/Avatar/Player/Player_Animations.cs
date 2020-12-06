using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player_Animations : MonoBehaviour, IPlayerAnimator
{
    private Animator animator;
    private Vector2 AnimationInput;
    private PlayerInput _PlayerInput;
    private Rigidbody2D RBCharacter;
    private bool grounded = false;
    private bool Jumped = false;
    void Awake()
    {
        RBCharacter = GetComponent<Rigidbody2D>();
        _PlayerInput = GetComponent<PlayerInput>();
        animator = _PlayerInput.GetComponentInChildren<Animator>();
        if(!animator)
        {
            Debug.Log("Player's Animator Not Found " + transform.name);
        }
    }

    private void Update()
    {
        grounded = _PlayerInput.Grounded;
        Jumped = _PlayerInput.Jumped;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        AnimationInput.x = _PlayerInput.Player_Input.x;
        AnimationInput.y = Mathf.Clamp(RBCharacter.velocity.y, 0, 1);
        ApplyAnimation();
    }

    public void ApplyAnimation()
    {
        animator.SetFloat("InputX", AnimationInput.x);
        animator.SetFloat("InputY", AnimationInput.y);
        animator.SetBool("Jump", Jumped);
        animator.SetBool("Grounded", grounded);
    }
}
