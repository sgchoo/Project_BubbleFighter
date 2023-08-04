using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 작업 영역 추가
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

    //판넬정보
    public GameObject login_pannel;
    public GameObject lobby_pannel;
    public GameObject room_pannel;

    //로그인정보 변수
    public InputField nick_nameset;

    //로비 정보 변수
    public InputField room_name;
    public Text nickname;

    // 방에서 채팅을 침
    public Text[] chat_text; //채팅용 글자들..

    // 채팅의 글을 관리... 추가, 삭제 (list)
    List<RoomInfo> myList = new List<RoomInfo>();

    //방내의 정보
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
        if(nick_nameset.text.Length == 0)   // 입력안하면
        {
            PhotonNetwork.NickName = "접속인";
        }

        else
        {
            PhotonNetwork.NickName = nick_nameset.text;
        }

        PhotonNetwork.ConnectUsingSettings(); // 접속시도
    }

    public override void OnConnectedToMaster()
    {
        // 접속이 되었을때
        PhotonNetwork.JoinLobby();  // 네트워크 접속시 로비로 입장
    }

    public override void OnJoinedLobby()    // 로비 접속 성공시
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
            print("방만들어짐?");
            // 방이름을 막적은것임
            room_name.text = "Room" + Random.Range(0, 100);
        }
        
        PhotonNetwork.CreateRoom(room_name.text,new RoomOptions { MaxPlayers=4 });    // 방을 만드는데..
        
    }

    public void RandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    // 방접속성공
    public override void OnJoinedRoom()
    {
        print("방이만들어졌음");
        // 랜덤 방 참여 실패
        lobby_pannel.SetActive(false);
        room_pannel.SetActive(true);
        RoomInfoText.text = "";
        Room_Renual();
        if (PhotonNetwork.IsMasterClient)
        {
            print("방장됨");
        }

        else
        {
        }
        
    }


    void Room_Renual()
    {
        RoomInfoText.text = "Room : " + PhotonNetwork.CurrentRoom.Name + "\n" + "(" + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers + ")";
    }

    //랜덤 방 참여 실패
    public override void OnJoinRandomFailed(short returnCode, string message)   //랜덤방 합류 실패
    {
        room_name.text = ""; // 이름때문에 안되거나 없거나
        CreateRoom();
    }
    // 방접속실패

    public override void OnCreateRoomFailed(short returnCode, string message)   //생성시 에러
    {
        room_name.text = ""; // 이름때문에 안되거나 없거나
        CreateRoom();   //방생성
    }

    //채팅관련
    public void MySendMessage()
    {
        photonView.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + ":" + chat_input.text);



    }
    [PunRPC]
    void ChatRPC(string msg)    //"개구리 : 물이 좋아"
    {
        bool is_input = false;
        print(msg);
        for(int i = 0; i < chat_text.Length; i++)
            if (chat_text[i].text == "") // 내부요소가 비었으면
            {
                chat_text[i].text = msg;
                break;
            }
        // 공간이 있으면.. true, 채팅창이 다 차면 false 상태 그대로 내려옴
        if (is_input == false)
        {
            for (int i = 1; i < chat_text.Length; i++)
            {
                chat_text[i - 1].text = chat_text[i].text; // 1칸 올려서 text 교체..
            }
            chat_text[chat_text.Length - 1].text = msg;
        }
    }

    //방빠져나가기

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Room_Renual();
        ChatRPC(otherPlayer.NickName + "님이 퇴장했습니다");
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
        // 현재 클릭된 버튼을 가져옵니다.
        GameObject clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        // 클릭된 버튼이 없거나 이미 비활성화된 경우, 작업을 중단합니다.
        if (clickedButton == null || !clickedButton.activeSelf)
            return;

        // 현재 클릭된 버튼을 다른 클라이언트에도 동기화합니다.
        photonView.RPC("DisableButton", RpcTarget.All, clickedButton.name);

    }

    [PunRPC]
    private void DisableButton1(string buttonName)
    {
        // 버튼 이름을 기반으로 해당 버튼을 찾습니다.
        GameObject button = GameObject.Find(buttonName);

        // 버튼을 비활성화합니다.
        if (button != null)
            button.SetActive(false);


    }

}
