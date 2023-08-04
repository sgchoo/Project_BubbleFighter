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

    public bool isMaster;    //���� ���� üũ
    
    NetworkState nState;

    //�α��� ��ư�� �������� ��Ʈ��ũ ���� �õ� -> ��Ȳ�� �Լ����� �ڵ����� ȣ���� �� : CallBack�Լ�
    //�ش� �Լ��� �߰����� ����̳� �缳���� ���� ���ϴ� ��Ȳ���� �ֵ�
    private void Start()
    {
        infoText.text = "";

        nState = NetworkState.None;

        //��Ʈ��ũ ���� ����
        PhotonNetwork.GameVersion = "0.1";
        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 30;
        PhotonNetwork.AutomaticallySyncScene = true;

        //ó�� ���۽� ������ �ȵǾ� �����Ƿ� ���� �� ��� ���� ��ư�� ��Ȱ��ȭ
        logoutBtn.interactable = false;
        createBtn.interactable = false;
        goBtn.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (nState == NetworkState.Inroom)
        {
            infoText.text = "������ : �ο� " + PhotonNetwork.PlayerList.Length;
        }
    }

    //��ư�� ������ ������ �Լ���
    public void Connect_Server()    //��ư���� ���� �õ�
    {
        PhotonNetwork.ConnectUsingSettings();    //���� ���� �õ�
        infoText.text = "���� �õ���";
    }

    public override void OnConnectedToMaster()   //���� ������ ����������
    {
        infoText.text = "���� ����";

        nState = NetworkState.Connect;

        logoutBtn.interactable = true;
        createBtn.interactable = true;
        loginBtn.interactable = false;
    }

    public override void OnDisconnected(DisconnectCause cause)  //�ش� ������ ���߾� ������ ���θ� ����
    {
        if (nState == NetworkState.Disconnect)            //�ڹ��� ����
        {
            infoText.text = "�α׾ƿ� ����, ������ ����";
        }
        else                                              //���ڹ��� ����
        {
            infoText.text = "�������� ����...��������";
            PhotonNetwork.ConnectUsingSettings();  //�ٽ� ���� ���� �õ�
        }
    }

    public void Disconnect_Server()    //��ư���� ���� ����
    {
        PhotonNetwork.Disconnect();

        logoutBtn.interactable = false;
        createBtn.interactable = false;
        loginBtn.interactable = true;

        nState = NetworkState.Disconnect;
    }

    //��ư�� ������ ���������� ���� ������ �����
    public void Connect_Room()
    {
        if (PhotonNetwork.IsConnected)
        {
            infoText.text = "������ �غ� ��";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        infoText.text = "�� ���� ����....�� ���� ��";
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
        infoText.text = "������ : �ο� " + PhotonNetwork.PlayerList.Length;

        if (isMaster == true)
        {
            //Go��ư Ȱ��ȭ
            goBtn.gameObject.SetActive(true);
        }
    }
}
