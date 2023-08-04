using UnityEngine;

public class MouseClickSound : MonoBehaviour
{
    public AudioClip clickSound; // Ŭ�� ȿ���� ����� Ŭ��

    private AudioSource audioSource; // ����� �ҽ� ������Ʈ

    

    private void Start()
    {
        // ����� �ҽ� ������Ʈ ��������
        audioSource = GetComponent<AudioSource>();

        // ����� Ŭ�� ����
        audioSource.clip = clickSound;
    }

    private void Update()
    {
        // ���콺 ���� ��ư�� Ŭ������ ��
        if (Input.GetMouseButtonDown(0))
        {
            // Ŭ�� ȿ���� ���
            audioSource.Play();
            
        }
    }
    
}


