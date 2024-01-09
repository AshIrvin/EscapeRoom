using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _unlockedAudio;
    [SerializeField] private AudioSource _footsteps;
    [SerializeField] private AudioSource _music;

    private void Start()
    {
        GameManager.OnCompletion += PlayUnlockedExit;
    }

    public void PlaySound(AudioSource audioSource)
    {
        audioSource.Play();
    }

    private void PlayUnlockedExit(bool state)
    {
        PlaySound(_unlockedAudio);
    }
}
