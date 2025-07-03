using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip walkSound;
    public AudioClip jumpSound;
    public AudioClip attack1Sound;
    public AudioClip attack2Sound;
    public AudioClip attack3Sound;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float walkVolume = 1f;
    [Range(0f, 1f)] public float jumpVolume = 1f;
    [Range(0f, 1f)] public float attack1Volume = 1f;
    [Range(0f, 1f)] public float attack2Volume = 1f;
    [Range(0f, 1f)] public float attack3Volume = 1f;

    [Header("Step Settings")]
    public float walkStepInterval = 0.5f;

    private Rigidbody2D rb;
    private Animator animator;

    private float walkTimer = 0f;
    private bool isGrounded;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        if (Mathf.Abs(rb.linearVelocity.x) > 0.1f && isGrounded)
        {
            walkTimer -= Time.deltaTime;
            if (walkTimer <= 0f)
            {
                PlaySound(walkSound, walkVolume);
                walkTimer = walkStepInterval;
            }
        }
        else
        {
            walkTimer = 0f;
        }
    }

    public void PlayJumpSound()
    {
        PlaySound(jumpSound, jumpVolume);
    }

    public void PlayAttackSound(int attackIndex)
    {
        switch (attackIndex)
        {
            case 1:
                PlaySound(attack1Sound, attack1Volume);
                break;
            case 2:
                PlaySound(attack2Sound, attack2Volume);
                break;
            case 3:
                PlaySound(attack3Sound, attack3Volume);
                break;
        }
    }

    private void PlaySound(AudioClip clip, float volume)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip, volume);
        }
    }
}
