using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player_Animations : MonoBehaviour, IPlayerAnimator
{
    private Animator animator;
    private Vector2 AnimationInput;
    private PlayerInput _PlayerInput;
    void Awake()
    {
        _PlayerInput = GetComponent<PlayerInput>();
        animator = _PlayerInput.GetComponentInChildren<Animator>();
        if(!animator)
        {
            Debug.Log("Player's Animator Not Found " + transform.name);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AnimationInput.x = _PlayerInput.Player_Input.x;
        AnimationInput.y = _PlayerInput.Player_Input.y;
        ApplyAnimation();
    }

    public void ApplyAnimation()
    {
        animator.SetFloat("InputX", AnimationInput.x);
        animator.SetFloat("InputY", AnimationInput.y);
    }
}
