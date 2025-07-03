using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log($"Player took {damage} damage, current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("Player died.");

        if (animator != null)
        {
            animator.SetTrigger("death");  // Assuming your Animator has a "death" trigger
        }

        // Disable player control script if you have one
        PlayerController pc = GetComponent<PlayerController>();
        if (pc != null) pc.enabled = false;

        // Disable other scripts or rigidbody velocity if needed
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        // You can also disable collider or the script itself if you want:
        // Collider2D col = GetComponent<Collider2D>();
        // if(col != null) col.enabled = false;

        Destroy(gameObject, 5f);
    }
}