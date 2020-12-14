using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IEnemy
{
    public int Health { get { return health; } set { health = value; } }

    public void TakeDamage(int Damage)
    {
        Health -= Damage;
        Anim.SetBool("Hit", true);
        IsMoving = false;
        if (health <= 0 && !gameOver)
        {
            gameOver = true;
            IsMoving = false;
            Anim.SetBool("IsDead", true);
            isDead = true;
        }
    }
}
