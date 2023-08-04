using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerControl : MonoBehaviourPun, IPunObservable
{
    Vector3 sendPos;
    Quaternion sendRot;

    //�̵� ȸ�� �ӵ� �ִϸ��̼�
    float moveSpeed = 3.0f;
    float rotSpeed = 120f;

    //�ִϸ��̼� - ����
    Animator anim;

    //��ī�� -> �� �ִϸ��̼� speed
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
        //�̵�  + ȸ�� -> Mine, Remote => PhotonView���� üũ\
        if (photonView.IsMine)    //�� Ŭ���̾�Ʈ�� �� ��ü
        {
            float rot = Input.GetAxis("Horizontal");
            float dir = Input.GetAxis("Vertical");
            transform.Translate(Vector3.forward * dir * Time.deltaTime * moveSpeed);
            transform.Rotate(Vector3.up * rot * Time.deltaTime * rotSpeed);

            //�ִϸ��̼��� ���� ����
            setSpeed = Mathf.Abs(dir);
            anim.SetFloat("speed", setSpeed);
        }
        else                       //���� Ŭ���̾�Ʈ�� �� ��ü
        {
            //�� ������ ���ڿ������� ������(��������, ����)
            //this.transform.position = sendPos;
            //this.transform.rotation = sendRot;
            this.transform.position = Vector3.Lerp(this.transform.position, sendPos, Time.deltaTime * 20f);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, sendRot, Time.deltaTime * 20f);
            anim.SetFloat("speed", setSpeed);   //observed�� OnPhotonSerializeView�Լ����� �޾ƿ� ������
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Mine���¿����� �����͸� �Ѱ��ְ� -> save
        if (stream.IsWriting == true)
        {
            stream.SendNext(this.transform.position);
            stream.SendNext(this.transform.rotation);
            stream.SendNext(this.setSpeed);
        }
        //Remote���� �� ���� Mine��ü�� �� �����͸� �Ѱܹ޴´�. -> load
        if (stream.IsReading == true)
        {
            sendPos = (Vector3)stream.ReceiveNext();
            sendRot = (Quaternion)stream.ReceiveNext();
            this.setSpeed = (float)stream.ReceiveNext();
        }
    }
}
