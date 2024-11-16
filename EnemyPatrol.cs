using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform pointA; // Ѳ�ߵ� A
    public Transform pointB; // Ѳ�ߵ� B
    public bool isPatroling = true;
    private Transform target; // ��ǰĿ���

    public MoveToTarget moveToTarget;
    public EnemySight enemySight;
    public EnemyLookAround enemyLookAround;

    private bool isWaitingForTurn = false; // ��ֹ�ظ�����Э��

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
        moveToTarget.target = target; // ��ʼ����Ŀ��
    }

    void Update()
    {
        if (!enemySight.PlayerInside && moveToTarget.target == null && !isWaitingForTurn)
        {
            isWaitingForTurn = true; // ����Ϊ���ڵȴ�
            StartCoroutine(DelayBeforeTurning());
        }
    }

    private IEnumerator DelayBeforeTurning()
    {
        // ����ӳټ���
        enemyLookAround.enabled = true;
        float delay = Random.Range(3f, 6f);
        yield return new WaitForSeconds(delay);
        enemyLookAround.enabled = false;
        // �л�Ŀ���
        target = target == pointA ? pointB : pointA;
        moveToTarget.target = target;

        isWaitingForTurn = false; // ���ñ�־
    }
}
