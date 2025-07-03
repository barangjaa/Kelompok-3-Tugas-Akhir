using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Patrolling")]
    public Transform[] patrolPoints;
    private int currentPatrolIndex;
    private NavMeshAgent agent;

    [Header("Attack")]
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;
    public int attackDamage = 10;

    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Animation")]
    private Animator animator;

    private Transform player;
    private bool isDead = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentHealth = maxHealth;

        if (patrolPoints.Length == 0)
        {
            Debug.LogWarning("No patrol points assigned.");
        }

        currentPatrolIndex = 0;
        GoToNextPatrolPoint();
    }

    void Update()
    {
        if (isDead)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            agent.isStopped = true;
            FaceTarget(player.position);
            Attack();
        }
        else
        {
            Patrol();
        }

        UpdateAnimation();
    }

    private void Patrol()
    {
        agent.isStopped = false;

        if (patrolPoints.Length == 0)
            return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    private void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    private void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            animator.SetTrigger("Attack");

            // Damage the player if it has a health component (example)
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }

    private void FaceTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0; // keep only horizontal rotation
        if (direction.magnitude == 0)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void UpdateAnimation()
    {
        // If dead
        if (isDead)
        {
            animator.SetBool("IsDead", true);
            return;
        }

        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);

        if (speed < 0.1f)
        {
            // Idle animation handled automatically by setting Speed ~ 0
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Could add hit reaction animation here
        }
    }

    private void Die()
    {
        isDead = true;
        agent.isStopped = true;
        animator.SetBool("IsDead", true);

        // Disable collider or further interactions
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        // Optionally destroy enemy after delay
        Destroy(gameObject, 5f);
    }
}