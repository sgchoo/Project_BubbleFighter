using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// static bool 값을 지정하여
/// 물풍선에 닿았을 시 bool 변수 true로 바꿔주고
/// player스크립트에서 띄워지는 기능 추가
/// SingleTon으로 지정해줘도 될듯? 변수가 많지 않다면?
/// 물풍선 해제 기능 추가 시 거기에 해당 bool 변수 false로 바꿔주고
/// 위치/회전값, RigidBody 컴포넌트 값 원복시켜야함
/// </summary>

public class GameManager_Choo : MonoBehaviour
{
    //물풍선에 갇혔는지 확인용 bool 변수
    public static bool isInWater;

    void Start()
    {
        isInWater = false;
    }
}
