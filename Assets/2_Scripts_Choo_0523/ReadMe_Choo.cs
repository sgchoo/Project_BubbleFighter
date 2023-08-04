using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadMe_Choo : MonoBehaviour
{
    /// <summary>
    /// PlayerFire_Choo
    ///  -> 마우스 왼쪽 클릭 시
    ///  -> 물풍선 발사
    ///  
    /// BombAction_Choo
    ///  -> 앞으로 물리적인 힘 적용
    ///  -> Ground에 닿을 시 ExplosionEff생성 후
    ///  -> Destroy
    ///  
    /// WaterBombExplosion_Choo
    ///  -> OverlapSphere 함수 사용
    ///  -> 닿은 Player객체의 물풍선 자식 객체 활성화
    ///  -> Block이면 해당 객체 삭제
    ///  
    /// GameManager_Choo
    ///  -> static bool 변수 생성
    /// 
    /// PlayerControl_Choo
    ///  -> GameManager의 bool값이 true면 
    ///  -> 위치값, 회전값, RigidBody의 useGravity값 변화
    /// </summary>
}
