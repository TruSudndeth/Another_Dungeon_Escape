using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //ToDo's
    //
    [SerializeField]
    protected float Speed = 1;
    [SerializeField]
    protected int health = 100;
    [SerializeField]
    protected int Gems = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    public abstract void Update();
    public abstract void Attack();
}
