using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PathListMoveTo : MonoBehaviour
{
    public List<Transform> pathPoints; // 所有路径点
    public MoveToTarget target; // 目标移动脚本
    private int currentPointIndex = -1; // 当前路径点索引

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

            // 检查目标的 target 是否为 null
            if (target.target == null)
            {
                // 更新到下一个路径点
                currentPointIndex++;
                if (currentPointIndex >= pathPoints.Count)
                {
                    enemyLookAround.enabled = false;
                    enemyLookAtPlayer.enabled = true;
                    enemySight.enabled = true;
                    enemyPatrol.enabled = true;
                    pathPoints.Clear();
                    target.isRun = false;
                    currentPointIndex = -1; // 如果到达末尾，循环到起点
                    this.enabled = false;
                    return;
                }
                target.target = pathPoints[currentPointIndex]; // 设置下一个目标

            }
        }
    }
}
