using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    int regular_Swing = 10;
    private BoxCollider2D SwordCollider;
    private bool isEnabled_State = false;

    private void Awake()
    {
        SwordCollider = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        IEnemy hit = other.GetComponent<IEnemy>();
        if (hit != null)
        {
            SwordCollider.enabled = false;
            hit.TakeDamage(regular_Swing);
            hit.Attacked();
        }
    }

    public void ToggleBoxCollider()
    {
        isEnabled_State = !isEnabled_State;
        if(SwordCollider.enabled != isEnabled_State)
        {
            SwordCollider.enabled = isEnabled_State;
        }
    }
}
