using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void PlayOnButtonPressSound()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
}
