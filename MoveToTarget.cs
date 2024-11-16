using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public Transform target; // 目标对象
    public Transform TurnTarget;
    public EnemyAnimatorManager manager;
    public EnemyLookAround enemyLookAround;
    public bool isRun = false;
    public float speed = 2f; // 移动速度
    public float runSpeed = 6f;
    public float turnSpeed = 5f; // 转向速度
    public float runturnSpeed = 15f; // 转向速度
    public float stopDistance = 1.2f; // 目标附近的停止距离

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
            // 计算目标方向
            Vector3 direction = target.position - TurnTarget.transform.position;
            direction.y = 0; // 保持 Y 轴为 0

            // 检查与目标的距离
            if (direction.magnitude <= stopDistance || target == null)
            {
                target = null; // 将 target 设置为 null
                manager.inputX = 0;
                manager.inputY = 0;
                manager.isRun = isRun;
                return; // 结束更新，不再移动
            }

            // 平滑转向目标
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            TurnTarget.transform.rotation = Quaternion.Slerp(TurnTarget.transform.rotation, targetRotation, (isRun ? runturnSpeed : turnSpeed) * Time.deltaTime);

            // 移动到目标位置
            Vector3 moveDirection = direction.normalized * (isRun ? runSpeed : speed) * Time.deltaTime;
            manager.inputX = 0;
            manager.inputY = 1;
            manager.isRun = isRun;
            TurnTarget.transform.position += moveDirection;
        }
    }
}
