using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// �۾� ���� �߰�
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Photon.Chat;

public class Network_mgr_JMW : MonoBehaviourPunCallbacks
{
    public static  new PhotonView photonView;


    //Button char1;

    //public Button start_btn;
    //public Button ready_btn;

   
    //public bool isMaster;

    public Button EnterBtn;

    //�ǳ�����
    public GameObject login_pannel;
    public GameObject lobby_pannel;
    public GameObject room_pannel;

    //�α������� ����
    public InputField nick_nameset;

    //�κ� ���� ����
    public InputField room_name;
    public Text nickname;

    // �濡�� ä���� ħ
    public Text[] chat_text; //ä�ÿ� ���ڵ�..

    // ä���� ���� ����... �߰�, ���� (list)
    List<RoomInfo> myList = new List<RoomInfo>();

    //�泻�� ����
    public Text RoomInfoText;
    public InputField chat_input;


    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        photonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        login_pannel.SetActive(true);
        lobby_pannel.SetActive(false);
        room_pannel.SetActive(false);

        //start_btn.interactable = false;
        //ready_btn.interactable = false;

        
    }

    public void Connect_login()
    {
        if(nick_nameset.text.Length == 0)   // �Է¾��ϸ�
        {
            PhotonNetwork.NickName = "������";
        }

        else
        {
            PhotonNetwork.NickName = nick_nameset.text;
        }

        PhotonNetwork.ConnectUsingSettings(); // ���ӽõ�
    }

    public override void OnConnectedToMaster()
    {
        // ������ �Ǿ�����
        PhotonNetwork.JoinLobby();  // ��Ʈ��ũ ���ӽ� �κ�� ����
    }

    public override void OnJoinedLobby()    // �κ� ���� ������
    {
        login_pannel.SetActive(false);
        lobby_pannel.SetActive(true);
        room_pannel.SetActive(false);

        nickname.text = PhotonNetwork.NickName;

    }
    public void CreateRoom()
    {
        if(room_name.text =="")
        {
            print("�游�����?");
            // ���̸��� ����������
            room_name.text = "Room" + Random.Range(0, 100);
        }
        
        PhotonNetwork.CreateRoom(room_name.text,new RoomOptions { MaxPlayers=4 });    // ���� ����µ�..
        
    }

    public void RandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    // �����Ӽ���
    public override void OnJoinedRoom()
    {
        print("���̸��������");
        // ���� �� ���� ����
        lobby_pannel.SetActive(false);
        room_pannel.SetActive(true);
        RoomInfoText.text = "";
        Room_Renual();
        if (PhotonNetwork.IsMasterClient)
        {
            print("�����");
        }

        else
        {
        }
        
    }


    void Room_Renual()
    {
        RoomInfoText.text = "Room : " + PhotonNetwork.CurrentRoom.Name + "\n" + "(" + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers + ")";
    }

    //���� �� ���� ����
    public override void OnJoinRandomFailed(short returnCode, string message)   //������ �շ� ����
    {
        room_name.text = ""; // �̸������� �ȵǰų� ���ų�
        CreateRoom();
    }
    // �����ӽ���

    public override void OnCreateRoomFailed(short returnCode, string message)   //������ ����
    {
        room_name.text = ""; // �̸������� �ȵǰų� ���ų�
        CreateRoom();   //�����
    }

    //ä�ð���
    public void MySendMessage()
    {
        photonView.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + ":" + chat_input.text);



    }
    [PunRPC]
    void ChatRPC(string msg)    //"������ : ���� ����"
    {
        bool is_input = false;
        print(msg);
        for(int i = 0; i < chat_text.Length; i++)
            if (chat_text[i].text == "") // ���ο�Ұ� �������
            {
                chat_text[i].text = msg;
                break;
            }
        // ������ ������.. true, ä��â�� �� ���� false ���� �״�� ������
        if (is_input == false)
        {
            for (int i = 1; i < chat_text.Length; i++)
            {
                chat_text[i - 1].text = chat_text[i].text; // 1ĭ �÷��� text ��ü..
            }
            chat_text[chat_text.Length - 1].text = msg;
        }
    }

    //�����������

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Room_Renual();
        ChatRPC(otherPlayer.NickName + "���� �����߽��ϴ�");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Room_Renual();
    }

    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Return))
        {
            EnterBtn.onClick.Invoke();
            ClearChatInput();
        }
    }

    void ClearChatInput()
    {
        //chat_input.text = string.Empty;
        chat_input.text = "";
    }

    public void GoNextScene()
    {
        PhotonNetwork.LoadLevel("MainGameScene");
    }

    public void SelectButton1()
    {
        // ���� Ŭ���� ��ư�� �����ɴϴ�.
        GameObject clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        // Ŭ���� ��ư�� ���ų� �̹� ��Ȱ��ȭ�� ���, �۾��� �ߴ��մϴ�.
        if (clickedButton == null || !clickedButton.activeSelf)
            return;

        // ���� Ŭ���� ��ư�� �ٸ� Ŭ���̾�Ʈ���� ����ȭ�մϴ�.
        photonView.RPC("DisableButton", RpcTarget.All, clickedButton.name);

    }

    [PunRPC]
    private void DisableButton1(string buttonName)
    {
        // ��ư �̸��� ������� �ش� ��ư�� ã���ϴ�.
        GameObject button = GameObject.Find(buttonName);

        // ��ư�� ��Ȱ��ȭ�մϴ�.
        if (button != null)
            button.SetActive(false);


    }

}
