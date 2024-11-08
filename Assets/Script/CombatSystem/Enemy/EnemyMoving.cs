using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f; 
    public float attackRange = 1.5f; 
    public float detectionRange = 5f; 
    public LayerMask playerLayer; 

    private Transform player;
    private Rigidbody2D rb;
    private GameManagers gameManagers;

    private bool isPlayerInRange;
    private bool isPlayerDetected;
    
    private Vector2 movementDirection;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 
        rb = GetComponent<Rigidbody2D>(); 
        gameManagers = GameManagers.instance; 
    }

    void Update()
    {
        if (gameManagers.IsGamePaused()) 
        {
            Debug.Log("Game is Paused");
            return; 
        }

        DetectPlayer(); 
        MoveTowardsPlayer(); 
    }

    void DetectPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position); 
        
        if (distanceToPlayer <= detectionRange) 
        {
            isPlayerDetected = true;
            isPlayerInRange = distanceToPlayer <= attackRange; 
        }
        else
        {
            isPlayerDetected = false;
            isPlayerInRange = false;
        }
    }

    void MoveTowardsPlayer()
    {
        if (isPlayerDetected && !isPlayerInRange)
        {
            movementDirection = (player.position - transform.position).normalized; 
            rb.velocity = new Vector2(movementDirection.x * moveSpeed, movementDirection.y * moveSpeed); 
        }
        else
        {
            rb.velocity = Vector2.zero; 
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange); 

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); 
    }
}