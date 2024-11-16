using UnityEngine;

public class EnemyLookAround : MonoBehaviour
{
    public float lookDuration = 1f; // 每次观察的持续时间
    public float lookSpeed = 2f; // 转动速度
    public float lookAngleRange = 45f; // 随机转动的角度范围

    private Quaternion targetRotation;
    private float elapsedTime = 0f; // 经过的时间

    void Start()
    {
        StartLooking(); // 不再检查启用状态，直接开始观察
    }

    void Update()
    {
        // 逐渐转向目标旋转
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / lookDuration;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, t);

        // 检查是否达到目标旋转
        if (t >= 1f)
        {
            // 确保最终旋转
            transform.rotation = targetRotation;
            elapsedTime = 0f; // 重置时间
            StartLooking(); // 开始下一个观察
        }
    }

    private void StartLooking()
    {
        // 随机选择一个新的目标旋转
        float randomYRotation = Random.Range(-lookAngleRange, lookAngleRange);
        targetRotation = Quaternion.Euler(0, randomYRotation, 0);
    }
}
