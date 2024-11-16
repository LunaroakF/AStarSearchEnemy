using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public Transform target; // Ŀ�����
    public Transform TurnTarget;
    public EnemyAnimatorManager manager;
    public EnemyLookAround enemyLookAround;
    public bool isRun = false;
    public float speed = 2f; // �ƶ��ٶ�
    public float runSpeed = 6f;
    public float turnSpeed = 5f; // ת���ٶ�
    public float runturnSpeed = 15f; // ת���ٶ�
    public float stopDistance = 1.2f; // Ŀ�긽����ֹͣ����

    private void Start()
    {
        TurnTarget = transform;
        manager = GetComponent<EnemyAnimatorManager>();
    }
    public void ForceQuit()
    {
        target = null;
    }

    void Update()
    {
        if (target != null)
        {
            enemyLookAround.enabled = false;
            // ����Ŀ�귽��
            Vector3 direction = target.position - TurnTarget.transform.position;
            direction.y = 0; // ���� Y ��Ϊ 0

            // �����Ŀ��ľ���
            if (direction.magnitude <= stopDistance || target == null)
            {
                target = null; // �� target ����Ϊ null
                manager.inputX = 0;
                manager.inputY = 0;
                manager.isRun = isRun;
                return; // �������£������ƶ�
            }

            // ƽ��ת��Ŀ��
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            TurnTarget.transform.rotation = Quaternion.Slerp(TurnTarget.transform.rotation, targetRotation, (isRun ? runturnSpeed : turnSpeed) * Time.deltaTime);

            // �ƶ���Ŀ��λ��
            Vector3 moveDirection = direction.normalized * (isRun ? runSpeed : speed) * Time.deltaTime;
            manager.inputX = 0;
            manager.inputY = 1;
            manager.isRun = isRun;
            TurnTarget.transform.position += moveDirection;
        }
    }
}
