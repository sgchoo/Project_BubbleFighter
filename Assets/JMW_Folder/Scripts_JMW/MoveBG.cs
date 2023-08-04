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
        // 현재 위치 가져오기
        Vector3 currentPosition = transform.position;

        // 이동 벡터 계산
        Vector3 moveVector = new Vector3(direction.x, direction.y, 0f) * speed * Time.deltaTime;

        // 새로운 위치 계산
        Vector3 newPosition = currentPosition - moveVector;

        // 새로운 위치로 이동
        transform.position = newPosition;

        
    }

   


}
