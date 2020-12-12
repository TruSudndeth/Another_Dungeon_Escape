using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moss_Giant : Enemy, IEnemy
{
    public override void TakeDamage()
    {
        health -= 25;
        base.TakeDamage();
    }
}
