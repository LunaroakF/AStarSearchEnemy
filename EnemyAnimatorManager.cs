using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorManager : MonoBehaviour
{
    public Animator animator;
    public HealthState healthState;
    public float inputX;
    public float inputY;
    public bool OnceKick = false;
    public bool OnceDeath = false;
    public bool isRun;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("inputX", inputX);
        animator.SetFloat("inputY", inputY);
        animator.SetBool("Run", isRun);
        animator.SetBool("Dead", OnceDeath);
        animator.SetBool("Attack",OnceKick);

        //if (enemyPatrol.isMoving || enemychase.isChase) {animator.SetFloat("inputX", 0); animator.SetFloat("inputY", 1); }
        //else { animator.SetFloat("inputX", 0); animator.SetFloat("inputY", 0); }
        ////if (enemyGravity.isjump) animator.SetTrigger("Jump");
    }
}
