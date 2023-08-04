using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// �����ǰ� ���� �� �����
/// OverlapSphere����ؼ�(������ 10f)
/// Collision�� ��ü �� Block�̶�� �ν���(�����)
/// Player��� Bubble��ü Ȱ��ȭ�ǰ� �÷��̾���ġ,ȸ���� ����
/// </summary>

public class BombRadius
{
    public float explosionRadius = 10f;
}

public class WaterBombExplosion_Choo : MonoBehaviour
{
    BombRadius radius;
    GameObject player;
    public GameObject BombSphere;
    public AudioClip audio;
    public AudioSource audios;


    [SerializeField] GameObject[] items = new GameObject[2];

    //public float explosionRadius = 10f;
    float rotSpeed = 100f;
    float deadTime = 0f;
    public bool inWater;

    private void Start()
    {
        inWater = false;

        radius = new BombRadius();
        audios.PlayOneShot(audio);
    }
    void Update()
    {
        DetectObject(radius.explosionRadius);
    }
    //������ ���� ������ OverlapSphere�Լ�
    void DetectObject(float radius)
    {
        //�����Ǹ� OverlapSphere�� �ݰ� radius�� SphereCollider ����
        Collider[] colls = Physics.OverlapSphere(this.transform.position, radius);

        foreach (Collider coll in colls)
        {
            
            if (coll.gameObject.tag == "Player")
            {
                inWater = true;
                player = coll.gameObject;
                    coll.transform.GetComponent<CapsuleCollider>().isTrigger = true;
                    coll.attachedRigidbody.useGravity = false;
                
                    coll.transform.position = new Vector3(this.transform.position.x, 1f, this.transform.position.z);
                    coll.transform.GetChild(4).gameObject.SetActive(true);
                    deadTime += Time.deltaTime;


            }
            if (coll.gameObject.tag == "Block")
            {
                int ranNum = Random.Range(0, 10);
                int ranIndex = Random.Range(0, 2);

                if (ranNum < 4)
                {
                    GameObject item = Instantiate(items[ranIndex], coll.transform.position, coll.transform.rotation);
                    item.transform.position = coll.transform.position;

                }

                Destroy(coll.gameObject);
            }
        }
        Invoke("EffectDestroy", 2f);
    }
    void EffectDestroy()
    {
        Destroy(this.gameObject);

    }
}
