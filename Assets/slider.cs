using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class UISliderSFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip valueChangeSFX;

    private void Awake()
    {
        GetComponent<Slider>().onValueChanged.AddListener(PlaySFXOnChange);
    }

    void PlaySFXOnChange(float value)
    {
        if (valueChangeSFX != null && audioSource != null)
        {
            audioSource.PlayOneShot(valueChangeSFX);
        }
    }
}
