using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class RegularSwordSwing : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer CopyFlipStateSprite;
    [SerializeField]
    private Player_Animations playerAnimation;
    
    private SpriteRenderer spriteRender;
    private Animator anim;
    private bool flip = false;
    private bool flipState = false;
    private bool swing = false;
    private bool swingState = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        if (!spriteRender || !anim) Debug.Log("prerequisits missing" + transform.name);
    }
    void Update()
    {
        swing = playerAnimation.Swing;
        flip = CopyFlipStateSprite.flipX;
    }

    private void FixedUpdate()
    {
        if(StateChanged(ref swingState, swing))
        {
            anim.SetBool("Swing", swing);
        }
        if(StateChanged(ref flipState, flip))
        {
            spriteRender.flipX = flip;
            if (flip)
            {
                if (transform.localPosition.x > 0) FlipX();
            }
            else
                if (transform.localPosition.x < 0) FlipX();
        }
    }

    private void FlipX()
    {
        transform.localPosition *= new Vector2(-1, 1);
    }

    private bool StateChanged(ref bool CheckState, bool toVariable)
    {
        if (CheckState != toVariable)
        {
            CheckState = toVariable;
            return true;
        }
        else
            return false;
    }
}
