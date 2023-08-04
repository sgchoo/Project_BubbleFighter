using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ ��ǳ���� �����ԵǸ�(GameManager_Choo�� bool ���� true��)
/// ��ġ�� ȸ���� RigidBody useGravity�� ��ȭ
/// ���� ��ǳ���� ���ִ� ��� �߰��� �ű⿡ GameManager_Choo�� bool �� false
/// �׸��� ��ġ�� ȸ���� RigidBody useGravity�� �����ϱ�
/// </summary>

public class PlayerControl_Choo : MonoBehaviour
{
    //ȸ�� �ӵ� �Լ�
    float rotSpeed = 100f;

   // bool isInWater;

    float deadTime = 0f;

    Rigidbody rigid;

    void Start()
    {

        ValueInit();
    }

    void Update()
    {
        TrappedUnderwater();
    }

    void ValueInit()
    {
        rigid = GetComponent<Rigidbody>();
    }

    //��ǳ���� ������ �� ��ġ��, ȸ����, ������ٵ� �߷� false
    void TrappedUnderwater()
    {
        if (GameManager_Choo.isInWater)
        {
            //x,z���� �״������ y�ุ ���� �߰�
            this.transform.position = new Vector3(this.transform.position.x, 3f, this.transform.position.z);
            //������� õõ�� ȸ��(���� ���� ����, ������ ��)
            this.transform.Rotate(rotSpeed * Time.deltaTime, rotSpeed * Time.deltaTime, rotSpeed * Time.deltaTime);

            rigid.useGravity = false;
            deadTime += Time.deltaTime;
            if(deadTime > 5)
            {
                Destroy(this.gameObject);
                GameManager_Choo.isInWater = false;
            }
        }
    }
}
