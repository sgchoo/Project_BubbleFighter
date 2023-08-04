using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// �����Ǹ� ������ �������� �� ����
/// Ground�� ��� �Ǹ� ������(�������)
/// �׸��� ������ ����
/// </summary>

public class BombAction_Choo : MonoBehaviour
{
    float deadTime;

    //���� ����Ʈ(��ƼŬ)
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
