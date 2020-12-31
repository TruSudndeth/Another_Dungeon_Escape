using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon_Event : MonoBehaviour
{
    private EnemyWeapon enemyWeapon;
    // Start is called before the first frame update
    void Awake()
    {
        enemyWeapon = GetComponentInChildren<EnemyWeapon>();
    }

    public void ToggleCollider()
    {
        enemyWeapon.ToggleBoxCollider();
    }
}
