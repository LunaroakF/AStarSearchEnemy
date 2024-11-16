using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class EnemyChaseBreakTime : MonoBehaviour
{
    public float minChaseTime = 0.4f; // Chase time minimum value
    public float maxChaseTime = 2f; // Chase time maximum value
    public float minBreakTime = 2f;  // Break time minimum value
    public float maxBreakTime = 10f;  // Break time maximum value

    public bool isChase = false;

    public MoveToTarget moveToTarget;
    public EnemySight enemySight;
    public EnemyPatrol enemyPatrol;

    void OnEnable()
    {
        moveToTarget = GetComponent<MoveToTarget>();
        enemySight = GetComponent<EnemySight>();
        enemyPatrol = GetComponent<EnemyPatrol>();
        StartCoroutine(ChaseRoutine());
    }

    private void Update()
    {
        if (enemySight.enabled && !enemyPatrol.enabled && moveToTarget.target != null)
        {
            moveToTarget.isRun = isChase;
        }
    }

    IEnumerator ChaseRoutine()
    {
        while (true)
        {
            // 随机生成追逐和休息时间
            float chasePeriod = Random.Range(minChaseTime, maxChaseTime);
            float breakPeriod = Random.Range(minBreakTime, maxBreakTime);

            isChase = true;
            yield return new WaitForSeconds(chasePeriod);

            isChase = false;
            yield return new WaitForSeconds(breakPeriod);
        }
    }
}
