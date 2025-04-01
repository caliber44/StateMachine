using UnityEngine;

public class AttackState : IState
{
    private StateMachine m_stateMachine;

    private float m_attackTime = 1;
    private float m_currentAttackTime;
    private float m_minTimeToEnter = 1.5f;
    private float m_lastExitTime = -1;

    public bool CanTransition()
    {
        return ((m_lastExitTime + m_minTimeToEnter) < Time.time);
    }
    public void Enter()
    {
        Debug.Log("Attack Entered");

        m_currentAttackTime = m_attackTime;
    }
    public void Exit()
    {
        Debug.Log("Attack Exited");
    }
    public int GetKey()
    {
        return (int)TestStateID.ATTACK; // Unique ID for the state
    }
    public void Tick(float deltaTime)
    {
        Debug.Log("ATTCKING : ticked at " + deltaTime);

        m_currentAttackTime -= deltaTime;

        if (m_currentAttackTime <= 0)
        {
            m_stateMachine.RevertState();
        }
    }
    public void SetStateMachineManager(StateMachine stateMachine)
    {
        this.m_stateMachine = stateMachine;
    }

    public float GetTickRate()
    {
        return 0.08f; // Faster independent tick rates for attacking
    }
}
