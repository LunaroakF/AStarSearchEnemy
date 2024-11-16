using System.Collections.Generic;
using UnityEngine;

public class PathFingding : MonoBehaviour
{
    public Transform Center;
    public List<Transform> pathPoints; // 所有路径点
    public List<Transform> validPathPoints; // 存储有效的路径点

    void Update()
    {
        //CheckPathPoints();
    }

    public void CheckPathPoints()
    {
        validPathPoints = new List<Transform>();
        foreach (Transform pathPoint in pathPoints)
        {
            // 计算从玩家到路径点的方向
            Vector3 direction = pathPoint.position - Center.position;

            // 使用射线检测是否有障碍物
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
