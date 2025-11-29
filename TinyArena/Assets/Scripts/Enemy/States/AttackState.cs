using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer; 
    private float losePlayerTimer; 
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
        if (!enemy.IsPlayerInAttackRange() || !enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new PatrolState());
            return;
        }

        if (enemy.player != null)
        {
            Vector3 direction = (enemy.player.transform.position - enemy.transform.position).normalized;
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);
        }

        if (Time.time >= enemy.lastAttackTime + enemy.attackCooldown)
        {
            Attack();
            enemy.lastAttackTime = Time.time;
        }
    }

    public void Attack()
    {
        if (enemy.player != null)
        {
            PlayerHealth playerHealth = enemy.player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(enemy.attackDamage);
            }
        }
    }
}
