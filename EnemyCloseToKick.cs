using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCloseToKick : MonoBehaviour
{
    public EnemySight enemySight;
    public EnemyAnimatorManager animatorManager;
    public Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        enemySight = GetComponent<EnemySight>();
        animatorManager = GetComponent<EnemyAnimatorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemySight.PlayerInside && enemySight.PlayerObject == Player)
        {
            if (GetDistance(transform, enemySight.PlayerObject) <= 1.8f)
            {
                animatorManager.OnceKick = true;
            }
            else
            {
                animatorManager.OnceKick = false;
            }
        }
    }

    private float GetDistance(Transform A, Transform B)
    {
        Vector3 APosition = new Vector3(A.position.x, 0, A.position.z);
        Vector3 BPosition = new Vector3(B.position.x, 0, B.position.z);
        //Debug.Log(APosition + "Óë" + BPosition + "µÄ===¾àÀë:" + Vector3.Distance(APosition, BPosition));
        return Vector3.Distance(APosition, BPosition);
    }

}
