using System.Collections;
using UnityEngine;

public class EnemyGravity : MonoBehaviour
{
    public float gravityStrength = -9.81f; // 重力强度
    public float jumpForce = 8f; // 跳跃力度
    public LayerMask groundLayer; // 地面层
    public bool isjump = false;
    public float verticalVelocity = 0f; // 垂直速度
    public bool isGrounded; // 是否在地面上
    //private Collider collider; // 物体的碰撞体

    void Start()
    {
        //collider = GetComponent<Collider>();
    }

    void Update()
    {
        // 更新地面状态

        // 如果在地面上，重置垂直速度
        if (isGrounded)
        {
            verticalVelocity = 0f;

            // 处理跳跃
            if (isjump) // 使用空格键跳跃
            {
                verticalVelocity = jumpForce;
                StartCoroutine(TurnTonoJump());
            }
        }
        else
        {
            // 如果不在地面上，应用重力
            verticalVelocity += gravityStrength * Time.deltaTime;
        }

        // 更新位置
        Vector3 move = new Vector3(0, verticalVelocity * Time.deltaTime, 0);
        transform.position += move;

        // 确保物体不会穿透地面
        //if (transform.position.y < 0)
        //{
        //    transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        //    isGrounded = true; // 立即标记为在地面上
        //}
        CheckGrounded();

    }

    private void CheckGrounded()
    {
        // 使用射线检测地面
        RaycastHit hit;
        // 从物体的底部向下发射射线
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private IEnumerator TurnTonoJump()
    {
        float delay = 1f;
        yield return new WaitForSeconds(delay);
        isjump = false;
    }

}
