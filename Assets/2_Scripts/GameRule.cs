using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameRule : MonoBehaviourPunCallbacks
{

    public static int playerCnt;
    public static int bubbleTouchCnt = 0;


    public GameObject[] characterPrefabs; // ĳ���� �����յ�
    public Transform[] spawnPositions; // ���� ��ġ��
    public GameObject UIUI;

    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.AddCallbackTarget(this); // �ݹ� Ÿ������ ���

            PhotonNetwork.CurrentRoom.IsOpen = false; // ���� �߿��� ���� ���� ����
            PhotonNetwork.CurrentRoom.IsVisible = false; // ���� �߿��� ���� ǥ������ ����

            AllocateCharacter();
        }
    }
    private void Update()
    {
        if (bubbleTouchCnt == 3)
        {
            Debug.Log("End");
            print(bubbleTouchCnt);
            bubbleTouchCnt = 0;
            UIUI.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this); // �ݹ� Ÿ�ٿ��� ����
    }

    private void AllocateCharacter()
    {
        int playerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;

        if (playerIndex < 0 || playerIndex >= characterPrefabs.Length || playerIndex >= spawnPositions.Length)
        {
            Debug.LogWarning("�÷��̾� �ε����� �´� ĳ���ͳ� ��ġ�� �����ϴ�.");
            return;
        }

        GameObject characterPrefab = characterPrefabs[playerIndex];
        Transform spawnPosition = spawnPositions[playerIndex];

        GameObject character = PhotonNetwork.Instantiate(characterPrefab.name, spawnPosition.position, spawnPosition.rotation);
    }
}
