using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 물풍선에 갇히게되면(GameManager_Choo의 bool 값이 true면)
/// 위치값 회전값 RigidBody useGravity값 변화
/// 추후 물풍선을 없애는 기능 추가시 거기에 GameManager_Choo의 bool 값 false
/// 그리고 위치값 회전값 RigidBody useGravity값 원복하기
/// </summary>

public class PlayerControl_Choo : MonoBehaviour
{
    //회전 속도 함수
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

    //물풍선에 갇혔을 시 위치값, 회전값, 리지드바디 중력 false
    void TrappedUnderwater()
    {
        if (GameManager_Choo.isInWater)
        {
            //x,z축은 그대로지만 y축만 위로 뜨게
            this.transform.position = new Vector3(this.transform.position.x, 3f, this.transform.position.z);
            //띄워지고 천천히 회전(물에 갇힌 느낌, 지워도 댐)
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
