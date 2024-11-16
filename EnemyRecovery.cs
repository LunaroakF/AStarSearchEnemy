using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRecovery : MonoBehaviour
{
    public float RecoverPreFrame = 0.1f;

    public Collider targetCollider;
    public HealthState healthState;

    private void Start()
    {
        healthState=GetComponent<HealthState>();
    }

    void Update()
    {
        if (IsOverlapping())
        {
            healthState.Dodetal(RecoverPreFrame);
        }
    }

    private bool IsOverlapping()
    {
        // 获取本地 GameObject 的 Collider
        Collider localCollider = GetComponent<Collider>();
        if (localCollider != null && targetCollider != null)
        {
            // 检查两个 Collider 的边界是否重叠
            return localCollider.bounds.Intersects(targetCollider.bounds);
        }
        return false;
    }
}
