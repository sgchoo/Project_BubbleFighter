using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerControl : MonoBehaviourPun, IPunObservable
{
    Vector3 sendPos;
    Quaternion sendRot;

    //이동 회전 속도 애니메이션
    float moveSpeed = 3.0f;
    float rotSpeed = 120f;

    //애니메이션 - 제어
    Animator anim;

    //메카님 -> 각 애니메이션 speed
    float setSpeed = 0f;

    void Start()
    {
        anim = this.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        MoveRot();
    }

    void MoveRot()
    {
        //이동  + 회전 -> Mine, Remote => PhotonView에서 체크\
        if (photonView.IsMine)    //내 클라이언트의 내 객체
        {
            float rot = Input.GetAxis("Horizontal");
            float dir = Input.GetAxis("Vertical");
            transform.Translate(Vector3.forward * dir * Time.deltaTime * moveSpeed);
            transform.Rotate(Vector3.up * rot * Time.deltaTime * rotSpeed);

            //애니메이션을 위한 변수
            setSpeed = Mathf.Abs(dir);
            anim.SetFloat("speed", setSpeed);
        }
        else                       //남의 클라이언트의 내 객체
        {
            //값 셋팅이 부자연스러운 움직임(끊어진다, 지연)
            //this.transform.position = sendPos;
            //this.transform.rotation = sendRot;
            this.transform.position = Vector3.Lerp(this.transform.position, sendPos, Time.deltaTime * 20f);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, sendRot, Time.deltaTime * 20f);
            anim.SetFloat("speed", setSpeed);   //observed의 OnPhotonSerializeView함수에서 받아온 변수값
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Mine상태에서는 데이터를 넘겨주고 -> save
        if (stream.IsWriting == true)
        {
            stream.SendNext(this.transform.position);
            stream.SendNext(this.transform.rotation);
            stream.SendNext(this.setSpeed);
        }
        //Remote상태 일 때는 Mine객체가 준 데이터를 넘겨받는다. -> load
        if (stream.IsReading == true)
        {
            sendPos = (Vector3)stream.ReceiveNext();
            sendRot = (Quaternion)stream.ReceiveNext();
            this.setSpeed = (float)stream.ReceiveNext();
        }
    }
}
