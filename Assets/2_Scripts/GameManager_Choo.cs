using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// static bool ���� �����Ͽ�
/// ��ǳ���� ����� �� bool ���� true�� �ٲ��ְ�
/// player��ũ��Ʈ���� ������� ��� �߰�
/// SingleTon���� �������൵ �ɵ�? ������ ���� �ʴٸ�?
/// ��ǳ�� ���� ��� �߰� �� �ű⿡ �ش� bool ���� false�� �ٲ��ְ�
/// ��ġ/ȸ����, RigidBody ������Ʈ �� �������Ѿ���
/// </summary>

public class GameManager_Choo : MonoBehaviour
{
    //��ǳ���� �������� Ȯ�ο� bool ����
    public static bool isInWater;

    void Start()
    {
        isInWater = false;
    }
}
