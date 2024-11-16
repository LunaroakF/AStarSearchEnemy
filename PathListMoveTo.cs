using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PathListMoveTo : MonoBehaviour
{
    public List<Transform> pathPoints; // ����·����
    public MoveToTarget target; // Ŀ���ƶ��ű�
    private int currentPointIndex = -1; // ��ǰ·��������

    public EnemyLookAround enemyLookAround;
    public EnemyLookAtPlayer enemyLookAtPlayer;
    public EnemySight enemySight;
    public EnemyPatrol enemyPatrol;

    // Start is called before the first frame update
    private void Start()
    {
        target = GetComponent<MoveToTarget>();
        enemyLookAround = GetComponent<EnemyLookAround>();
        enemyLookAtPlayer = GetComponent<EnemyLookAtPlayer>();
        enemySight = GetComponent<EnemySight>();
        enemyPatrol = GetComponent<EnemyPatrol>();
    }
    void OnEnable()
    {
        target = GetComponent<MoveToTarget>();
        currentPointIndex = -1;

        enemyLookAround.enabled = false;
        enemyLookAtPlayer.enabled = false;
        enemySight.enabled = false;
        enemyPatrol.enabled = false;

        target.target = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (pathPoints.Count>0)
        {
            enemyLookAround.enabled = false;
            enemyLookAtPlayer.enabled = false;
            enemySight.enabled = false;
            enemyPatrol.enabled = false;
        }
        if (target != null && pathPoints.Count > 0)
        {
            target.isRun = true;

            // ���Ŀ��� target �Ƿ�Ϊ null
            if (target.target == null)
            {
                // ���µ���һ��·����
                currentPointIndex++;
                if (currentPointIndex >= pathPoints.Count)
                {
                    enemyLookAround.enabled = false;
                    enemyLookAtPlayer.enabled = true;
                    enemySight.enabled = true;
                    enemyPatrol.enabled = true;
                    pathPoints.Clear();
                    target.isRun = false;
                    currentPointIndex = -1; // �������ĩβ��ѭ�������
                    this.enabled = false;
                    return;
                }
                target.target = pathPoints[currentPointIndex]; // ������һ��Ŀ��

            }
        }
    }
}
