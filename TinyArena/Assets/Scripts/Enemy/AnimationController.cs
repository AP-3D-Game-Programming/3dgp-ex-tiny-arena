using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private StateMachine stateMachine;

    [SerializeField] private EnemyState previousState;

    private void Start()
    {
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<StateMachine>();
        previousState = stateMachine.activeStateType;
    }

    private void Update()
    {
        if (animator == null || stateMachine == null) return;

        if (stateMachine.activeStateType != previousState)
        {
            UpdateAnimations(stateMachine.activeStateType);
            previousState = stateMachine.activeStateType;
        }
    }

    private void UpdateAnimations(EnemyState state)
    {
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsPatrolling", false);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsJumping", false);

        // Set the current state
        switch (state)
        {
            case EnemyState.Idle:
                Debug.Log("Idle");
                animator.SetBool("IsIdle", true);
                break;
            case EnemyState.Patrol:
                Debug.Log("Patrolling");
                animator.SetBool("IsPatrolling", true);
                break;
            case EnemyState.Attack:
                Debug.Log("Attacking");
                animator.SetBool("IsAttacking", true);
                break;
            case EnemyState.Jump:
                animator.SetBool("IsJumping", true);
                break;
        }
    }

}