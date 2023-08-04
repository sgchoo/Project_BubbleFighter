using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

enum NetworkState
{
    None,
    Connect,
    Disconnect,
    MakeRoom,
    Inroom
}

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public Button loginBtn;
    public Button logoutBtn;
    public Button createBtn;
    public Button goBtn;
    public Text infoText;

    public bool isMaster;    //방장 여부 체크
    
    NetworkState nState;

    //로그인 버튼을 눌렀을때 네트워크 접속 시도 -> 상황별 함수들이 자동으로 호출이 됨 : CallBack함수
    //해당 함수에 추가적인 기능이나 재설정을 통해 원하는 상황으로 주도
    private void Start()
    {
        infoText.text = "";

        nState = NetworkState.None;

        //네트워크 셋팅 관련
        PhotonNetwork.GameVersion = "0.1";
        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 30;
        PhotonNetwork.AutomaticallySyncScene = true;

        //처음 시작시 접속이 안되어 있으므로 접속 후 기능 관련 버튼은 비활성화
        logoutBtn.interactable = false;
        createBtn.interactable = false;
        goBtn.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (nState == NetworkState.Inroom)
        {
            infoText.text = "방참가 : 인원 " + PhotonNetwork.PlayerList.Length;
        }
    }

    //버튼이 눌리면 수행할 함수들
    public void Connect_Server()    //버튼으로 접속 시도
    {
        PhotonNetwork.ConnectUsingSettings();    //서버 접속 시도
        infoText.text = "접속 시도중";
    }

    public override void OnConnectedToMaster()   //서버 접속이 성공했을때
    {
        infoText.text = "접속 성공";

        nState = NetworkState.Connect;

        logoutBtn.interactable = true;
        createBtn.interactable = true;
        loginBtn.interactable = false;
    }

    public override void OnDisconnected(DisconnectCause cause)  //해당 사유에 맞추어 재접속 여부를 결정
    {
        if (nState == NetworkState.Disconnect)            //자발적 해제
        {
            infoText.text = "로그아웃 상태, 재접속 가능";
        }
        else                                              //비자발적 해제
        {
            infoText.text = "오프라인 상태...재접속중";
            PhotonNetwork.ConnectUsingSettings();  //다시 서버 접속 시도
        }
    }

    public void Disconnect_Server()    //버튼으로 접속 해제
    {
        PhotonNetwork.Disconnect();

        logoutBtn.interactable = false;
        createBtn.interactable = false;
        loginBtn.interactable = true;

        nState = NetworkState.Disconnect;
    }

    //버튼을 누르면 랜덤방으로 들어가고 없으면 만들기
    public void Connect_Room()
    {
        if (PhotonNetwork.IsConnected)
        {
            infoText.text = "방접속 준비 중";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        infoText.text = "빈 방이 없음....방 생성 중";
        isMaster = true;
        PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = 3});
    }

   public void GoNextScene()
    {
        PhotonNetwork.LoadLevel("MainGameScene");
    }

    public override void OnJoinedRoom()
    {
        nState = NetworkState.Inroom;
        infoText.text = "방참가 : 인원 " + PhotonNetwork.PlayerList.Length;

        if (isMaster == true)
        {
            //Go버튼 활성화
            goBtn.gameObject.SetActive(true);
        }
    }
}
