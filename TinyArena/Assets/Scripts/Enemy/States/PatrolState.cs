using UnityEngine;

public class PatrolState : BaseState
{
    public int waypointIndex;
    public float waitTimer;
    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        enemy.MoveToPlayer();
        if (enemy.IsPlayerInAttackRange() && enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }
    }

}
