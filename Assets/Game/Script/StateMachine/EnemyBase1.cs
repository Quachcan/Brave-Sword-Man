using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase1 : MonoBehaviour, IDamageable
{
    public FiniteStateMachine stateMachine;

    public D_EnemyBase enemyBaseData;
    public int facingDirection { get; private set; }   

    public Rigidbody2D rb {get; private set;}
    public Animator animator{get; private set;}
    public GameObject aliveGO {get; private set;}
    public AnimationToStateMachine atsm {get; private set;}
    
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;

    private Vector2 velocityWorkSpace;



    private void Awake()
    {
        aliveGO = transform.Find("Enemy").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        animator = aliveGO.GetComponent<Animator>(); 
        atsm = aliveGO.GetComponent<AnimationToStateMachine>();
    }

    public virtual void Start()
    {
        facingDirection = 1;
        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currenState.LogicUpdate();
    }

    public virtual void FixedUpdate ()
    {
        stateMachine.currenState.PhysicUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkSpace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkSpace;
    }

    public virtual bool CheckWall()
    {
        if (wallCheck != null || aliveGO != null)
        {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, enemyBaseData.wallCheckDistance, enemyBaseData.whatIsGround);
        }
        else
        {
            return false;
        }
    }


    public virtual bool CheckLedge()
    {
        if (ledgeCheck != null || aliveGO != null)
        {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, enemyBaseData.ledgeCheckDistance, enemyBaseData.whatIsGround);
        }
        else
        {
            return false;
        }
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        if (playerCheck != null || aliveGO != null)
        {
            return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, enemyBaseData.minAgroDistance, enemyBaseData.whatIsPlayer );
        }
        else 
        {
            return false;
        }
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        if (playerCheck != null || aliveGO != null)
        {
            return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, enemyBaseData.maxAgroDistance, enemyBaseData.whatIsPlayer);
        }
        else
        {
            return false;
        }
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        if (playerCheck != null || aliveGO != null)
        {
            return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, enemyBaseData.closeRangeActionDistance, enemyBaseData.whatIsPlayer);
        }
        else
        {
            return false;
        }
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
    }

    public virtual void OnDrawGizmos()
    {

        if (playerCheck != null)
        {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * enemyBaseData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * enemyBaseData.ledgeCheckDistance));

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * enemyBaseData.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * enemyBaseData.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * enemyBaseData.maxAgroDistance), 0.2f);
        }
        else 
        {
            return;
        }
    }
    public void OnHit()
    {

    }

    public void OnDead()
    {

    }
}
