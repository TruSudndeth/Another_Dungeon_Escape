﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player_Animations : MonoBehaviour, IPlayerAnimator
{
    private Animator animator;
    private Rigidbody2D RBCharacter;
    private PlayerInput _PlayerInput;
    private SpriteRenderer spriteRender;
    
    private bool Jumped = false;
    private bool grounded = false;
    private bool FlipSprite = false;
    private int InvertAnimation = 1;
    private Vector2 AnimationInput;
    void Awake()
    {
        RBCharacter = GetComponent<Rigidbody2D>();
        _PlayerInput = GetComponent<PlayerInput>();
        spriteRender = GetComponentInChildren<SpriteRenderer>();
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
        AnimationInput.x = _PlayerInput.Player_Input.x * InvertAnimation;
        FlipX();
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

    private void FlipX()
    {
        if (AnimationInput.x != 0)
        {

            if (!_PlayerInput.FlipModifyer)
            {
                if (AnimationInput.x < 0)
                {
                    AnimationInput.x *= -1;
                    spriteRender.flipX = true;
                    if (spriteRender.transform.localPosition.x > 0) spriteRender.transform.localPosition *= new Vector2(-1, 1);
                }
                else
                {
                    AnimationInput.x *= 1;
                    spriteRender.flipX = false;
                    if (spriteRender.transform.localPosition.x < 0) spriteRender.transform.localPosition *= new Vector2(-1, 1);
                } 
            }

            if (_PlayerInput.FlipModifyer && spriteRender.flipX)
            {
                InvertAnimation = -1;
            }
            else
                InvertAnimation = 1;
        }
    }
}
