using UnityEngine;

public class PlayerComboAttack : MonoBehaviour
{
    [Header("Combo Settings")]
    public float comboResetTime = 0.8f;
    public float attackRange = 0.5f;
    public int attackDamage = 10;  // Increased for balance with EnemyAI
    public Transform attackPoint;
    public LayerMask enemyLayers;

    private int comboStep = 0;
    private float lastAttackTime = 0f;
    private bool isAttacking = false;

    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip hurtClip;
    public AudioClip deathClip;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    private Animator animator;
    private Rigidbody2D rb;
    private PlayerAudioManager audioManager;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioManager = GetComponent<PlayerAudioManager>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead) return;

        // Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // Reset combo if reset time exceeded
        if (Time.time - lastAttackTime > comboResetTime)
        {
            comboStep = 0;
            isAttacking = false;
        }

        // Attack input
        if (Input.GetKeyDown(KeyCode.J) && !isAttacking)
        {
            lastAttackTime = Time.time;
            comboStep++;
            isAttacking = true;

            if (comboStep == 1)
            {
                animator.SetTrigger("Attack1");
                audioManager?.PlayAttackSound(1);
            }
            else if (comboStep == 2)
            {
                animator.SetTrigger("Attack2");
                audioManager?.PlayAttackSound(2);
            }
            else
            {
                animator.SetTrigger("Attack3");
                audioManager?.PlayAttackSound(3);
                comboStep = 0;
            }

            // Detect enemies and deal damage
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider2D enemyCollider in hitEnemies)
            {
                EnemyAI enemyAI = enemyCollider.GetComponent<EnemyAI>();
                if (enemyAI != null)
                {
                    enemyAI.TakeDamage(attackDamage);
                    Debug.Log($"Hit {enemyCollider.name}: -{attackDamage} HP");
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (hurtClip != null)
            audioSource.PlayOneShot(hurtClip);

        animator.SetTrigger("hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        animator.SetTrigger("death");

        if (deathClip != null)
            audioSource.PlayOneShot(deathClip);

        PlayerController pc = GetComponent<PlayerController>();
        if (pc != null)
            pc.enabled = false;

        this.enabled = false;
    }

    public void EndAttack()
    {
        isAttacking = false;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}