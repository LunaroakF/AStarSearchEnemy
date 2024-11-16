using UnityEngine;

public class EnemyLookAround : MonoBehaviour
{
    public float lookDuration = 1f; // ÿ�ι۲�ĳ���ʱ��
    public float lookSpeed = 2f; // ת���ٶ�
    public float lookAngleRange = 45f; // ���ת���ĽǶȷ�Χ

    private Quaternion targetRotation;
    private float elapsedTime = 0f; // ������ʱ��

    void Start()
    {
        StartLooking(); // ���ټ������״̬��ֱ�ӿ�ʼ�۲�
    }

    void Update()
    {
        // ��ת��Ŀ����ת
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / lookDuration;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, t);

        // ����Ƿ�ﵽĿ����ת
        if (t >= 1f)
        {
            // ȷ��������ת
            transform.rotation = targetRotation;
            elapsedTime = 0f; // ����ʱ��
            StartLooking(); // ��ʼ��һ���۲�
        }
    }

    private void StartLooking()
    {
        // ���ѡ��һ���µ�Ŀ����ת
        float randomYRotation = Random.Range(-lookAngleRange, lookAngleRange);
        targetRotation = Quaternion.Euler(0, randomYRotation, 0);
    }
}
