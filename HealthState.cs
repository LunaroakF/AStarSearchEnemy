using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthState : MonoBehaviour
{
    public float MaxBlood = 100f;
    public float HealthBlood = 100f;
    public EnemyAnimatorManager EnemyAnimatorManager;

    public LowHealthRecoverPath lowHealthRecoverPath;
    // Start is called before the first frame update
    void Start()
    {
        EnemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
        lowHealthRecoverPath = GetComponent<LowHealthRecoverPath>();
    }

    public void Dodetal(float health)
    {
        HealthBlood += health;
        if (HealthBlood >= 40 && lowHealthRecoverPath!=null)
        { 
            lowHealthRecoverPath.enabled = true;
        }
        if (HealthBlood > MaxBlood) HealthBlood = MaxBlood;
        if (HealthBlood <= 0)
        {
            health = 0;
            Dead();
        }
    }

    private void Dead()
    {
        EnemyLookAround enemyLookAround = GetComponent<EnemyLookAround>();
        MoveToTarget moveToTarget = GetComponent<MoveToTarget>();
        EnemyLookAtPlayer enemyLookAtPlayer = GetComponent<EnemyLookAtPlayer>();
        EnemySight enemySight = GetComponent<EnemySight>();
        EnemyPatrol enemyPatrol = GetComponent<EnemyPatrol>();
        EnemyRecovery enemyRecovery = GetComponent<EnemyRecovery>();
        PathFingding pathFingding = GetComponent<PathFingding>();
        AStarSearch aStarSearch = GetComponent<AStarSearch>();
        PathListMoveTo pathListMoveTo = GetComponent<PathListMoveTo>();
        lowHealthRecoverPath = GetComponent<LowHealthRecoverPath>();    
        EnemyChaseBreakTime enemyChaseBreakTime = GetComponent<EnemyChaseBreakTime>();
        EnemyCloseToKick enemyCloseToKick = GetComponent<EnemyCloseToKick>();
        CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();

        if (enemyLookAround) enemyLookAround.enabled = false;
        if (moveToTarget) moveToTarget.enabled = false;
        if (enemyLookAtPlayer) enemyLookAtPlayer.enabled = false;
        if (enemySight) enemySight.enabled = false;
        if (enemyPatrol) enemyPatrol.enabled = false;
        if (enemyRecovery) enemyRecovery.enabled = false;
        if (pathFingding) pathFingding.enabled = false;
        if (aStarSearch) aStarSearch.enabled = false;
        if (pathListMoveTo) pathListMoveTo.enabled = false;
        if (lowHealthRecoverPath) lowHealthRecoverPath.enabled = false;
        if (enemyChaseBreakTime) enemyChaseBreakTime.enabled = false;
        if (enemyCloseToKick) enemyCloseToKick.enabled = false;
        if(capsuleCollider) capsuleCollider.enabled = false;

        EnemyAnimatorManager.OnceDeath = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (HealthBlood > 30 && lowHealthRecoverPath!= null)
        {
            lowHealthRecoverPath.enabled = true;
        }
    }
}
