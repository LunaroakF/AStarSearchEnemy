using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    public Transform playerHead;
    public Transform enemyHead;
    public float lookSpeed = 5f;
    public float maxAngle = 90f; // 最大旋转角度
    private Quaternion resetRotation;

    void Start()
    {
        resetRotation = enemyHead.localRotation; // 使用局部旋转
    }

    void OnDisable()
    {
        enemyHead.localRotation = resetRotation; // 重新设置敌人头部的局部旋转
    }

    void Update()
    {
        Vector3 direction = playerHead.position - enemyHead.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // 将目标旋转转换为局部旋转
            Quaternion localTargetRotation = Quaternion.Inverse(enemyHead.parent.rotation) * targetRotation;

            // 计算当前旋转与目标旋转之间的角度差
            float angleDifference = Quaternion.Angle(resetRotation, localTargetRotation);

            // 如果角度差超过最大角度限制，则限制到最大角度
            if (angleDifference > maxAngle)
            {
                // 计算限制后的旋转
                float t = maxAngle / angleDifference; // 计算比例
                localTargetRotation = Quaternion.Slerp(resetRotation, localTargetRotation, t);
            }

            // 进行平滑旋转
            enemyHead.localRotation = Quaternion.Slerp(enemyHead.localRotation, localTargetRotation, lookSpeed * Time.deltaTime);
        }
    }
}
