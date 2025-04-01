using UnityEngine;

public class RunState : IState
{
    private StateMachine m_stateMachine;

    public bool CanTransition()
    {
        return true;
    }
    public void Enter()
    {
        Debug.Log("Run Entered");
    }

    public void Tick(float deltaTime)
    {
        Debug.Log("Run ticked at " + deltaTime);

        if (!Input.GetKey(KeyCode.Space)) 
        {
            m_stateMachine.ChangeState((int)TestStateID.IDLE);
        }
        if (Input.GetKey(KeyCode.LeftControl)) 
        {
            m_stateMachine.ChangeState((int)TestStateID.RUNNING_ATTACK);
        }

    }

    public void Exit()
    {
        Debug.Log("Run Exited");
    }

    public int GetKey()
    {
        return (int)TestStateID.RUN; // Unique ID for the state
    }

    public void SetStateMachineManager(StateMachine stateMachine)
    {
        this.m_stateMachine = stateMachine;
    }

    public float GetTickRate()
    {
        return 0.16f; // Independent tick rates
    }
}

