using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 생성되면 앞으로 물리적인 힘 적용
/// Ground에 닿게 되면 터진다(사라진다)
/// 그리고 프리펩 생성
/// </summary>

public class BombAction_Choo : MonoBehaviour
{
    float deadTime;

    //폭발 이펙트(파티클)
    public GameObject explosionEff;

    Rigidbody rigid;

    void Awake()
    {
        
    }

    void Start()
    {
        rigid = this.GetComponent<Rigidbody>();
        rigid.AddForce(transform.forward * 25f, ForceMode.Impulse);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            deadTime += Time.deltaTime;

            if (deadTime > 2f)
            {
                deadTime = 0f;
                GameObject explosion = Instantiate(explosionEff);
                explosion.transform.position = this.transform.position;
                Destroy(this.gameObject);
            }
        }
    }
}
