using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IEnemy
{
    public override void TakeDamage()
    {
        health -= 10;
        base.TakeDamage();
    }
}
