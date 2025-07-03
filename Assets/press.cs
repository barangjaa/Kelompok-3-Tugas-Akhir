using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UISFXPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSFX; // Hanya satu clip

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(PlayClickSound);
    }

    void PlayClickSound()
    {
        if (clickSFX != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSFX);
        }
    }
}
