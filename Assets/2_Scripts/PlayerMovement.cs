using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMove
{
    public float runSpeed = 20f;
}

public class PlayerMovement : MonoBehaviourPun, IPunObservable
{
    //캐릭터 애니메이션
    Animator player_Anim;

    Camera camera;
    Rigidbody cR;
    PlayerMove move;

    Vector3 sendPos;
    Quaternion sendRot;

    
    float runValue = 0f;
    //public float runSpeed = 20.0f;

    bool gDown;

    public float smoothnesss = 10f;

    private void Awake()
    {
        player_Anim = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        camera = Camera.main;

        move = new PlayerMove();

        cR = this.GetComponent<Rigidbody>();
    }
    void Update()
    {
        InputMovement();
        GetInput();
    }
    void LateUpdate()
    {
        Vector3 playerRotate = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothnesss);
    }

    void GetInput()
    {
        gDown = Input.GetButtonDown("Fire1");
    }

    void InputMovement()
    {
        if (photonView.IsMine)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            Vector3 moveDir = forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal");

            cR.velocity = (moveDir * move.runSpeed);

            runValue = moveDir.magnitude;

            player_Anim.SetFloat("runValue", runValue);

            if (gDown)
            {
                player_Anim.SetBool("isThrow", true);
            }
            else
            {
                player_Anim.SetBool("isThrow", false);
            }
        }

        else
        {
            this.transform.position = Vector3.Lerp(this.transform.position, sendPos, Time.deltaTime * 20f);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, sendRot, Time.deltaTime * 20f);
            
            player_Anim.SetFloat("runValue", runValue);

            if (gDown)
            {
                player_Anim.SetBool("isThrow", true);
            }
            else
            {
                player_Anim.SetBool("isThrow", false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "SpeedUpItem")
        {
            move.runSpeed += 2f;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Mine상태에서는 데이터를 넘겨주고 -> save
        if (stream.IsWriting == true)
        {
            stream.SendNext(this.transform.position);
            stream.SendNext(this.transform.rotation);
            stream.SendNext(this.runValue);
        }
        //Remote상태 일 때는 Mine객체가 준 데이터를 넘겨받는다. -> load
        if (stream.IsReading == true)
        {
            sendPos = (Vector3)stream.ReceiveNext();
            sendRot = (Quaternion)stream.ReceiveNext();
            this.runValue = (float)stream.ReceiveNext();
        }
    }
}
