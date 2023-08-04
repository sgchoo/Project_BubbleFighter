using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���콺 ���� Ŭ����
/// FirePosition����
/// ��ź ������ ����
/// </summary>

public class PlayerFire_Choo : MonoBehaviour
{
    public Transform throwPosition;
    public GameObject bombPrefab;

    void Update()
    {
        ThrowBomb();
    }

    //���콺 ��Ŭ���� ��ź ����(Ŭ�� ��ư ���� ����)
    void ThrowBomb()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bomb = Instantiate(bombPrefab);
            bomb.transform.position = throwPosition.position;
        }
    }
}
