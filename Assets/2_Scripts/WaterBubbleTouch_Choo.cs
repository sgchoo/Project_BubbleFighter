using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEditor.Il2Cpp;

public class WaterBubbleTouch_Choo : MonoBehaviourPun
{

    private void Update()
    {
        PlayerMove move = new PlayerMove();
        move.runSpeed = 0;
        this.transform.parent.gameObject.transform.position = new Vector3 (this.transform.position.x, 1f, this.transform.position.z);
        this.transform.parent.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            this.gameObject.SetActive(false);
            this.transform.parent.GetComponent<CapsuleCollider>().enabled = false;
            this.transform.parent.GetChild(0).gameObject.SetActive(false);
            GameRule.bubbleTouchCnt++;
        }
    }
}
