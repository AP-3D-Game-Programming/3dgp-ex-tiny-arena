using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }
    [SerializeField]
    private string currentState;
    public GameObject player;
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    public float eyeHeight = 0.6f;

    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Attack Settings")]
    public float attackRange = 2f;
    public float attackDamage = 10f;
    public float attackCooldown = 1.5f;
    [HideInInspector] public float lastAttackTime;

    [Header("Visual Feedback")]
    public Color damageFlashColor = Color.red;
    public float flashDuration = 0.1f;

    private Renderer[] renderers;
    private Color[] originalColors;

    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");

        currentHealth = maxHealth;

        // Store renderers and original colors for flash effect
        renderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].material.HasProperty("_Color"))
            {
                originalColors[i] = renderers[i].material.color;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
    }

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo = new();
                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.gameObject == player)
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public void MoveToPlayer()
    {
        if (player != null && agent != null)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    public bool IsPlayerInAttackRange()
    {
        if (player == null) return false;
        return Vector3.Distance(transform.position, player.transform.position) <= attackRange;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Enemy took {damage} damage! Health: {currentHealth}/{maxHealth}");

        // Flash red when taking damage
        FlashDamage();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void FlashDamage()
    {
        // Change to flash color
        foreach (Renderer renderer in renderers)
        {
            if (renderer.material.HasProperty("_Color"))
            {
                renderer.material.color = damageFlashColor;
            }
        }

        // Reset color after flash duration
        Invoke(nameof(ResetColor), flashDuration);
    }

    void ResetColor()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i] != null && renderers[i].material.HasProperty("_Color"))
            {
                renderers[i].material.color = originalColors[i];
            }
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }
}