using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spider_Attack_Event : MonoBehaviour
{
    [SerializeField]
    private GameObject ShootAcid;
    [SerializeField]
    private float speed = 10f;
    private GameObject acidFired;
    private Vector3 Direction;
    private Vector3 offsetPosition;
    public void AcidAttack()
    {
        FireDirection();
        acidFired = Instantiate(ShootAcid, transform.position + offsetPosition, Quaternion.identity);
        acidFired.GetComponent<Rigidbody2D>().velocity = Direction; 
    }
    private void FireDirection()
    {
        if (transform.localScale.x == 1)
        {
            Direction = Vector3.right * speed * Time.deltaTime;
            offsetPosition = new Vector3(-0.5f, 0f, 0f);
        }
        else
        {
            Direction = Vector3.left * speed * Time.deltaTime;
            offsetPosition = new Vector3(0.5f, 0f, 0f);
        }
    }
}
