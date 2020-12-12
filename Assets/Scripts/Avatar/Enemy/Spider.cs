using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy , IEnemy
{
    public override void TakeDamage()
    {
        health -= 50;
        base.TakeDamage();
    }
}
