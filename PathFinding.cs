using System.Collections.Generic;
using UnityEngine;

public class PathFingding : MonoBehaviour
{
    public Transform Center;
    public List<Transform> pathPoints; // ����·����
    public List<Transform> validPathPoints; // �洢��Ч��·����

    void Update()
    {
        //CheckPathPoints();
    }

    public void CheckPathPoints()
    {
        validPathPoints = new List<Transform>();
        foreach (Transform pathPoint in pathPoints)
        {
            // �������ҵ�·����ķ���
            Vector3 direction = pathPoint.position - Center.position;

            // ʹ�����߼���Ƿ����ϰ���
            if (!Physics.Raycast(Center.position, direction, direction.magnitude, LayerMask.GetMask("Level")))
            {
                if (pathPoint != transform)
                {
                    validPathPoints.Add(pathPoint);
                }
                }
        }
    }
}
