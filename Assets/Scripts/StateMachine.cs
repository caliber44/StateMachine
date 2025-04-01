using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    public void Enter();
    public void Tick(float deltatime);
    public void Exit();
    public int GetKey();
    public void SetStateMachineManager(StateMachine stateMachine);
}
public class StateMachine : MonoBehaviour
{
    [SerializeField] private float m_fixedTickTime;

    private Dictionary<int, IState> m_states = new Dictionary<int, IState>();
    private IState m_currentState;
    private IState m_previousState;
    private float m_accumulatedTime;

    private void Update()
    {
        m_accumulatedTime += Time.deltaTime;

        while (m_accumulatedTime >= m_fixedTickTime)
        {
            m_accumulatedTime -= m_fixedTickTime;
            Tick(m_fixedTickTime);
        }
    }

    private void Tick(float deltaTime)
    {
        if (m_currentState != null)
        {
            m_currentState.Tick(deltaTime);
        }
    }
    public void AddState(IState state) 
    {
        m_states.Add(state.GetKey(), state);
        state.SetStateMachineManager(this);
    }
    public void ChangeState(int id) 
    {

        if (m_states.TryGetValue(id, out IState state))
        {
            m_currentState.Exit();
            m_previousState = m_currentState;
            m_currentState = state;
            m_currentState.Enter();
        }
    }
    public void RevertState() 
    {
        m_currentState.Exit();
        IState temp = m_currentState;
        m_currentState = m_previousState;
        m_previousState = temp;
        m_currentState.Enter();
    }
    
}
