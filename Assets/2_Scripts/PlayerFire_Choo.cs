using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 마우스 왼쪽 클릭시
/// FirePosition에서
/// 폭탄 프리펩 생성
/// </summary>

public class PlayerFire_Choo : MonoBehaviour
{
    public Transform throwPosition;
    public GameObject bombPrefab;

    void Update()
    {
        ThrowBomb();
    }

    //마우스 좌클릭시 폭탄 생성(클릭 버튼 변경 가능)
    void ThrowBomb()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bomb = Instantiate(bombPrefab);
            bomb.transform.position = throwPosition.position;
        }
    }
}
