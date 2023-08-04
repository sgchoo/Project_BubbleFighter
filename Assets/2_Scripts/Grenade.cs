using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject meshObj;
    public GameObject effectOobj;
    public Rigidbody rigid;

    void Start()
    {
        StartCoroutine(Explosion());
    }
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(2f);
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        meshObj.SetActive(false);
        effectOobj.SetActive(true);
        Destroy(this.gameObject, 3);

    }
}
