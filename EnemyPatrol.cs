using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform pointA; // 巡逻点 A
    public Transform pointB; // 巡逻点 B
    public bool isPatroling = true;
    private Transform target; // 当前目标点

    public MoveToTarget moveToTarget;
    public EnemySight enemySight;
    public EnemyLookAround enemyLookAround;

    private bool isWaitingForTurn = false; // 防止重复启动协程

    private void Start()
    {
        enemyLookAround = GetComponent<EnemyLookAround>(); 
        enemySight = GetComponent<EnemySight>();
        moveToTarget = GetComponent<MoveToTarget>();
    }

    void OnEnable()
    {
        enemyLookAround = GetComponent<EnemyLookAround>();
        enemySight = GetComponent<EnemySight>();
        moveToTarget = GetComponent<MoveToTarget>();
        float distanceToA = Vector3.Distance(transform.position, pointA.position);
        float distanceToB = Vector3.Distance(transform.position, pointB.position);
        enemySight.enabled = true;
        target = distanceToA < distanceToB ? pointA : pointB;
        moveToTarget.target = target; // 初始设置目标
    }

    void Update()
    {
        if (!enemySight.PlayerInside && moveToTarget.target == null && !isWaitingForTurn)
        {
            isWaitingForTurn = true; // 设置为正在等待
            StartCoroutine(DelayBeforeTurning());
        }
    }

    private IEnumerator DelayBeforeTurning()
    {
        // 随机延迟几秒
        enemyLookAround.enabled = true;
        float delay = Random.Range(3f, 6f);
        yield return new WaitForSeconds(delay);
        enemyLookAround.enabled = false;
        // 切换目标点
        target = target == pointA ? pointB : pointA;
        moveToTarget.target = target;

        isWaitingForTurn = false; // 重置标志
    }
}
