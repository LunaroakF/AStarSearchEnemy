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
        // ��ȡ���� GameObject �� Collider
        Collider localCollider = GetComponent<Collider>();
        if (localCollider != null && targetCollider != null)
        {
            // ������� Collider �ı߽��Ƿ��ص�
            return localCollider.bounds.Intersects(targetCollider.bounds);
        }
        return false;
    }
}
