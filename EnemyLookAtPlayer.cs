using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    public Transform playerHead;
    public Transform enemyHead;
    public float lookSpeed = 5f;
    public float maxAngle = 90f; // �����ת�Ƕ�
    private Quaternion resetRotation;

    void Start()
    {
        resetRotation = enemyHead.localRotation; // ʹ�þֲ���ת
    }

    void OnDisable()
    {
        enemyHead.localRotation = resetRotation; // �������õ���ͷ���ľֲ���ת
    }

    void Update()
    {
        Vector3 direction = playerHead.position - enemyHead.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // ��Ŀ����תת��Ϊ�ֲ���ת
            Quaternion localTargetRotation = Quaternion.Inverse(enemyHead.parent.rotation) * targetRotation;

            // ���㵱ǰ��ת��Ŀ����ת֮��ĽǶȲ�
            float angleDifference = Quaternion.Angle(resetRotation, localTargetRotation);

            // ����ǶȲ�����Ƕ����ƣ������Ƶ����Ƕ�
            if (angleDifference > maxAngle)
            {
                // �������ƺ����ת
                float t = maxAngle / angleDifference; // �������
                localTargetRotation = Quaternion.Slerp(resetRotation, localTargetRotation, t);
            }

            // ����ƽ����ת
            enemyHead.localRotation = Quaternion.Slerp(enemyHead.localRotation, localTargetRotation, lookSpeed * Time.deltaTime);
        }
    }
}
