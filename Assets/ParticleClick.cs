using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleClick : MonoBehaviour
{
    public GameObject partic1;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))  // ���콺 ���� ��ư Ŭ�� ����
        {
            Vector3 clickPosition = Input.mousePosition;  // ���콺 Ŭ�� ��ġ ��������
            clickPosition.z = 10;  // ��ƼŬ�� ���ϴ� Z�� ������ ����

            // Ŭ���� ��ġ�� ��ƼŬ ����
            GameObject par1 = Instantiate(partic1, Camera.main.ScreenToWorldPoint(clickPosition), Quaternion.identity);

            // ������ ��ƼŬ ���
            Destroy(par1, 2f);
            
        }
    }

}