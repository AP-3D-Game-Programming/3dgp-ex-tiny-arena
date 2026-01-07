using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    public EnemyState activeStateType { get; private set; }
    public void Initialise()
    {
        ChangeState(new PatrolState(), EnemyState.Patrol);
    }
    void Start()
    {

    }

    void Update()
    {
        if(activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState, EnemyState stateType)
    {
        if (activeState != null)
        {
            activeState.Exit();
        }
        activeState = newState;
        activeStateType = stateType;

        if (activeState != null)
        {
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
}

