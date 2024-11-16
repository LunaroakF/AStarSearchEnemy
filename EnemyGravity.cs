using System.Collections;
using UnityEngine;

public class EnemyGravity : MonoBehaviour
{
    public float gravityStrength = -9.81f; // ����ǿ��
    public float jumpForce = 8f; // ��Ծ����
    public LayerMask groundLayer; // �����
    public bool isjump = false;
    public float verticalVelocity = 0f; // ��ֱ�ٶ�
    public bool isGrounded; // �Ƿ��ڵ�����
    //private Collider collider; // �������ײ��

    void Start()
    {
        //collider = GetComponent<Collider>();
    }

    void Update()
    {
        // ���µ���״̬

        // ����ڵ����ϣ����ô�ֱ�ٶ�
        if (isGrounded)
        {
            verticalVelocity = 0f;

            // ������Ծ
            if (isjump) // ʹ�ÿո����Ծ
            {
                verticalVelocity = jumpForce;
                StartCoroutine(TurnTonoJump());
            }
        }
        else
        {
            // ������ڵ����ϣ�Ӧ������
            verticalVelocity += gravityStrength * Time.deltaTime;
        }

        // ����λ��
        Vector3 move = new Vector3(0, verticalVelocity * Time.deltaTime, 0);
        transform.position += move;

        // ȷ�����岻�ᴩ͸����
        //if (transform.position.y < 0)
        //{
        //    transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        //    isGrounded = true; // �������Ϊ�ڵ�����
        //}
        CheckGrounded();

    }

    private void CheckGrounded()
    {
        // ʹ�����߼�����
        RaycastHit hit;
        // ������ĵײ����·�������
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
