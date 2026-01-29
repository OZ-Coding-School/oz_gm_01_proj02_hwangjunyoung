
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int scoreValue = 10;          // 아이템 점수
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            // 오디도 매니저로 제어하기
            audioSource.mute = !AudioManager.sfxOn;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 점수 증가
            ScoreManager.Instance.AddScore(scoreValue);

            // 오디오 매니저 상태 확인 후 재생
            if (audioSource != null && AudioManager.sfxOn)
            {
                audioSource.Play();
            }

            // 사운드가 끝나고 아이템 제거
            if (audioSource != null && audioSource.clip != null)
            {
                Destroy(gameObject, audioSource.clip.length);
            }

            else
            {
                Destroy(gameObject);
            }
                
        }
    }
}