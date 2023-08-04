using UnityEngine;

public class MouseClickSound : MonoBehaviour
{
    public AudioClip clickSound; // 클릭 효과음 오디오 클립

    private AudioSource audioSource; // 오디오 소스 컴포넌트

    

    private void Start()
    {
        // 오디오 소스 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();

        // 오디오 클립 설정
        audioSource.clip = clickSound;
    }

    private void Update()
    {
        // 마우스 왼쪽 버튼을 클릭했을 때
        if (Input.GetMouseButtonDown(0))
        {
            // 클릭 효과음 재생
            audioSource.Play();
            
        }
    }
    
}


