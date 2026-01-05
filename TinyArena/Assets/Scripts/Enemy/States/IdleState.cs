using UnityEngine;

public class IdleState : BaseState
{
    public override void Enter()
    {
        enemy.Agent.isStopped = true;
    }

    public override void Exit()
    {
        enemy.Agent.isStopped = false;
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new PatrolState(), EnemyState.Patrol);
            return;
        }
    }
}