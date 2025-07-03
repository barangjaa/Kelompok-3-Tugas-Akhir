using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [Header("Audio Clip")]
    public AudioClip Backsound;
    public AudioClip Press;
    public AudioClip Attack;
    public AudioClip Hit;
    public AudioClip Walk;
    public AudioClip Jump;


    public void PlayMusic(AudioClip main)
    {
        musicSource.playOnAwake = true;
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
