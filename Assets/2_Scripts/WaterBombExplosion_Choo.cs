using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 생성되고 몇초 뒤 사라짐
/// OverlapSphere사용해서(반지름 10f)
/// Collision된 객체 중 Block이라면 부숴짐(사라짐)
/// Player라면 Bubble객체 활성화되고 플레이어위치,회전값 변경
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
    //반지름 조절 가능한 OverlapSphere함수
    void DetectObject(float radius)
    {
        //생성되면 OverlapSphere로 반경 radius인 SphereCollider 생성
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
