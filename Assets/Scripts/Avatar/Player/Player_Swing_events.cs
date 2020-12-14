using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Swing_events : MonoBehaviour
{
    private Player_Attack playerAttack;

    private void Awake()
    {
        playerAttack = GetComponentInChildren<Player_Attack>();
    }

    private void ToggleCollider()
    {
        playerAttack.ToggleBoxCollider();
    }
}
