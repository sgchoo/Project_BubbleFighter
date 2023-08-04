using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameRule : MonoBehaviourPunCallbacks
{

    public static int playerCnt;
    public static int bubbleTouchCnt = 0;


    public GameObject[] characterPrefabs; // 캐릭터 프리팹들
    public Transform[] spawnPositions; // 생성 위치들
    public GameObject UIUI;

    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.AddCallbackTarget(this); // 콜백 타겟으로 등록

            PhotonNetwork.CurrentRoom.IsOpen = false; // 입장 중에는 방을 열지 않음
            PhotonNetwork.CurrentRoom.IsVisible = false; // 입장 중에는 방을 표시하지 않음

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
        PhotonNetwork.RemoveCallbackTarget(this); // 콜백 타겟에서 제거
    }

    private void AllocateCharacter()
    {
        int playerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;

        if (playerIndex < 0 || playerIndex >= characterPrefabs.Length || playerIndex >= spawnPositions.Length)
        {
            Debug.LogWarning("플레이어 인덱스에 맞는 캐릭터나 위치가 없습니다.");
            return;
        }

        GameObject characterPrefab = characterPrefabs[playerIndex];
        Transform spawnPosition = spawnPositions[playerIndex];

        GameObject character = PhotonNetwork.Instantiate(characterPrefab.name, spawnPosition.position, spawnPosition.rotation);
    }
}
