using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStarSearch : MonoBehaviour
{
    public Transform TargetLandMark;

    public PathFingding pathFingding;
    public float CurrentCost = 0;
    public List<float> Cost = new List<float>();
    public List<Transform> orderedPathPoints;

    private Transform CurrentPoint;
    private float lastcost = 0;

    private void Start()
    {
        pathFingding = GetComponent<PathFingding>();
    }

    private void Update()
    {
        
    }

    private void OnEnable()
    {
        CurrentCost = 0;
        CurrentPoint = transform;
        orderedPathPoints.Clear();
        GenerateVaildPathsList();
    }

    private void GenerateVaildPathsList()
    {
        for (int p  = 0; p< 50; p++)
        {
            pathFingding.Center = CurrentPoint;
            Transform closestPoint = null;
            float closestDistance = float.MaxValue; 
            int seleted = 0;
            CurrentCost -= lastcost;
            pathFingding.CheckPathPoints();
            Cost.Clear();
            for (int i = 0; i< pathFingding.validPathPoints.Count;i++)
            {
                if (orderedPathPoints.Contains(pathFingding.validPathPoints[i]))
                {
                    Cost.Add(0);
                    continue; // ������ǰ����
                }
                
                float costa = GetDistance(TargetLandMark, pathFingding.validPathPoints[i]);
                Cost.Add(costa);
                float cost = GetDistance(CurrentPoint, pathFingding.validPathPoints[i]) + Cost[i] + CurrentCost;
                Debug.Log(CurrentPoint + "��" + pathFingding.validPathPoints[i] + "�ľ���:" + cost + "����:Distance = " + GetDistance(CurrentPoint, pathFingding.validPathPoints[i])
                    + "Cost = " + Cost[i]
                    + "CurrentCost = " + CurrentCost
                    + "i = " + i);
                // ��鵱ǰ�����Ƿ�С����֪����С����
                if (cost < closestDistance)
                {
                    closestDistance = cost; // ������С����
                    closestPoint = pathFingding.validPathPoints[i]; // ���������·����
                    seleted = i;
                }
                if (pathFingding.validPathPoints[i] == TargetLandMark)
                {
                    orderedPathPoints.Add(closestPoint);
                    CurrentPoint = closestPoint;
                    this.enabled = false;
                    return;
                }
            }
            orderedPathPoints.Add(closestPoint);
            lastcost = Cost[seleted];
            CurrentCost += closestDistance;
            CurrentPoint = closestPoint;
            if (closestPoint == TargetLandMark)
            {
                this.enabled = false;
                return;
            }
            //closestPoint; // ���ؾ�����С��·����
        }
    }

    private float GetDistance(Transform A, Transform B)
    {
        Vector3 APosition = new Vector3(A.position.x, 0, A.position.z);
        Vector3 BPosition = new Vector3(B.position.x, 0, B.position.z);
        //Debug.Log(APosition + "��" + BPosition + "��===����:" + Vector3.Distance(APosition, BPosition));
        return Vector3.Distance(APosition, BPosition);
    }

}
