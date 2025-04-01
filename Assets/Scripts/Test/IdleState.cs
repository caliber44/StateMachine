using UnityEngine;

public class IdleState : IState
{
    private StateMachine m_stateMachine;

    public bool CanTransition()
    {
        return true;
    }
    public void Enter()
    {
        Debug.Log("Idle Entered");
    }

    public void Tick(float deltaTime)
    {
        Debug.Log("Idle ticked at " + deltaTime);

        if (Input.GetKey(KeyCode.Space)) 
        {
            m_stateMachine.ChangeState((int)TestStateID.RUN);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            m_stateMachine.ChangeState((int)TestStateID.ATTACK);
        }

    }

    public void Exit()
    {
        Debug.Log("Idle Exited");
    }

    public int GetKey()
    {
        return (int)TestStateID.IDLE; // Unique ID for the state
    }

    public void SetStateMachineManager(StateMachine stateMachine)
    {
        this.m_stateMachine = stateMachine;
    }

    public float GetTickRate()
    {
        return 0.16f; //Independant tick rates
    }
}
