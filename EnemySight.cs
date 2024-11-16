using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public bool PlayerInside = false;
    public float viewRadius = 20f; // 视野半径
    public float chaseRadius = 50f; // 追逐视野半径
    public float viewAngle = 110f; // 视野角度
    public Transform PlayerObject;
    public LayerMask playerLayer; // 玩家层
    public LayerMask obstacleLayer; // 障碍物层
    public float CurrentRadius;

    public EnemyLookAtPlayer enemyLookAtPlayer;
    public EnemyPatrol enemyPatrol;
    public MoveToTarget toTarget;

    private void Start()
    {
        enemyPatrol = GetComponent<EnemyPatrol>();
        enemyLookAtPlayer = GetComponent<EnemyLookAtPlayer>();
    }

    private void OnEnable()
    {
        toTarget = GetComponent<MoveToTarget>();
    }

    public void ForceQuit()
    {
        toTarget = null;
        PlayerInside = false;
        this.enabled = false;
    }

    private void Update()
    {

        DetectPlayer();
        enemyLookAtPlayer.enabled = PlayerInside;
        if (PlayerInside)
        {
            enemyPatrol.enabled = false;
            toTarget.target = PlayerObject;
        }
        else 
        {
            enemyPatrol.enabled = true;
        }
    }

    private void DetectPlayer()
    {
        Collider[] playersInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, playerLayer);
        PlayerObject = null;
        PlayerInside = false;

        foreach (Collider player in playersInViewRadius)
        {
            Transform playerTransform = player.transform;
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToPlayer) < viewAngle / 2)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
                Vector3 rayOrigin = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z); // 调整Y轴位置
                if (!Physics.Raycast(rayOrigin, directionToPlayer, distanceToPlayer, obstacleLayer))
                {
                    CurrentRadius = chaseRadius;
                    PlayerInside = true;
                    PlayerObject = playerTransform;
                    //enemyLookAtPlayer.enabled = true;
                    // 在这里可以添加玩家被发现后的逻辑
                }
            }
        }
        if (!PlayerInside) { CurrentRadius = viewRadius; } //enemyLookAtPlayer.enabled = false;enemyLookAtPlayer.reset();  }
        }
}
