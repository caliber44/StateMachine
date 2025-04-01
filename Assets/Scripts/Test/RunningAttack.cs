using Unity.VisualScripting;
using UnityEngine;

public class RunningAttackState : IState
{
    private StateMachine m_stateMachine;
    private float m_attackTime = 1;
    private float m_minTimeToEnter = 1.5f;
    private float m_lastExitTime = -1;

    private float m_currentAttackTime;

    public bool CanTransition()
    {
        return ((m_lastExitTime + m_minTimeToEnter) < Time.time);
    }
    public void Enter()
    {
        Debug.Log("Running Attack Entered");

        m_currentAttackTime = m_attackTime;

    }

    public void Tick(float deltaTime)
    {
        Debug.Log("Running Attack ticked at " + deltaTime);

        m_currentAttackTime -= deltaTime;

        if (m_currentAttackTime <= 0)
        {
            m_stateMachine.RevertState();
        }

    }
    public void Exit()
    {
        Debug.Log("Running Attack Exited");
        m_lastExitTime = Time.time;
    }

    public int GetKey()
    {
        return (int)TestStateID.RUNNING_ATTACK; // Unique ID for the state
    }

    public void SetStateMachineManager(StateMachine stateMachine)
    {
        this.m_stateMachine = stateMachine;
    }

    public float GetTickRate()
    {
        return 0.08f; //Faster Independent tick rates while attacking
    }
}
