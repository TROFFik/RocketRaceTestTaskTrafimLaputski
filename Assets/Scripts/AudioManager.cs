using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip audioClipDead;
    [SerializeField] private AudioClip audioClipFire;
    [SerializeField] private AudioClip audioClipCoin;

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        TriggerHandler.singleton.coinAction += PlayClipCoin;
        PlayerHealth.singleton.DeadAction += PlayClipDead;
        InputController.singleton.ñlickAction += PlayClipFire;
    }

    private void PlayClipCoin()
    {
        audioSource.PlayOneShot(audioClipCoin);
    }

    private void PlayClipDead()
    {
        audioSource.PlayOneShot(audioClipDead);
    }

    private void PlayClipFire(bool StartPlay)
    {
        if (StartPlay)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}
