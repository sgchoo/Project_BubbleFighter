using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleClick : MonoBehaviour
{
    public GameObject partic1;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))  // 마우스 왼쪽 버튼 클릭 감지
        {
            Vector3 clickPosition = Input.mousePosition;  // 마우스 클릭 위치 가져오기
            clickPosition.z = 10;  // 파티클을 원하는 Z축 값으로 설정

            // 클릭한 위치에 파티클 생성
            GameObject par1 = Instantiate(partic1, Camera.main.ScreenToWorldPoint(clickPosition), Quaternion.identity);

            // 생성된 파티클 재생
            Destroy(par1, 2f);
            
        }
    }

}