using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBG : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction = new Vector2(1f, 0.35f).normalized;
    //Vector3 first_pos;

    
  

    void Start()
    {
        //first_pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ��ġ ��������
        Vector3 currentPosition = transform.position;

        // �̵� ���� ���
        Vector3 moveVector = new Vector3(direction.x, direction.y, 0f) * speed * Time.deltaTime;

        // ���ο� ��ġ ���
        Vector3 newPosition = currentPosition - moveVector;

        // ���ο� ��ġ�� �̵�
        transform.position = newPosition;

        
    }

   


}
