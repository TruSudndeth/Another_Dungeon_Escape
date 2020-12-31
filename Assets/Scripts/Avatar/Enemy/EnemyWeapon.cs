using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    private bool isEnabled_State = false;
    private BoxCollider2D SwordCollider;
    // Start is called before the first frame update
    void Awake()
    {
        SwordCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SwordCollider.enabled = false;
            Debug.Log("PlayerHit");
        }
    }
    public void ToggleBoxCollider()
    {
        isEnabled_State = !isEnabled_State;
        if (SwordCollider.enabled != isEnabled_State)
        {
            SwordCollider.enabled = isEnabled_State;
        }
    }
}
