using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnB : MonoBehaviour
{
    [SerializeField]
    public Transform PointA;
    [SerializeField]
    public Transform PointB;
    private Vector2 CurrentPoint;
    private bool _PointA = true;

    private void Awake()
    {
        SetPoint();
    }
    public void FarthestPoint(Transform AI)
    {
        float Point1 = Vector2.Distance(AI.position, PointA.position);
        float Point2 = Vector2.Distance(AI.position, PointB.position);
        if (Point1 >= Point2)
        {
            _PointA = true;
        }
        else
        {
            _PointA = false;
        }
        SetPoint();
    }

    public Vector2 MoveToPoint(Vector2 AI)
    {
        Vector2 moveTowards = Vector2.zero;
        moveTowards = CurrentPoint - AI;
        if(Mathf.Round(moveTowards.x) == 0)
        {
            _PointA = !_PointA;
            SetPoint();
        }
        return moveTowards.normalized;
    }

    private void SetPoint()
    {
        if (_PointA)
            CurrentPoint = PointA.position;
        else
            CurrentPoint = PointB.position;
    }
}
