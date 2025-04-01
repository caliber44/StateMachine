using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IState 
{
    public void Enter();
    public void Tick(float deltatime);
    public void Exit();
    public int GetKey();
    public float GetTickRate();
    public bool CanTransition();
    public void SetStateMachineManager(StateMachine stateMachine);
}
public class StateMachine : MonoBehaviour
{
    [SerializeField] private bool isPlaying = true;

    private Dictionary<int, IState> m_states = new Dictionary<int, IState>();
    private IState m_currentState;
    private IState m_previousState;
    private float m_accumulatedTime;
    private float m_fixedTickTime;

    // Global speed scale (1 = normal speed, <1 = slower, >1 = faster)
    public float TimeScale = 1f;

    public void Play()
    {
        isPlaying = true;
    }
    public void Pause()
    {
        isPlaying = false;
    }
    private void Update()
    {
        if (!isPlaying || m_currentState == null) return;

        Tick();
    }
    private void Tick()
    {
        m_accumulatedTime += Time.deltaTime * TimeScale;

        while (m_accumulatedTime >= m_fixedTickTime)
        {
            m_accumulatedTime -= m_fixedTickTime;
            m_currentState.Tick(m_fixedTickTime);
        }
    }
    public bool AddState(IState state) 
    {
        if (m_states.ContainsKey(state.GetKey()))
        {
            return false;
        }

        m_states.Add(state.GetKey(), state);
        state.SetStateMachineManager(this);

        return true;
    }
    public void ChangeState(int id, bool forceTransition = false) 
    {
        if (m_states.TryGetValue(id, out IState state))
        {
            if (!forceTransition && !state.CanTransition()) return; //Check to see if state is ready i.e on cooldown

            if (m_currentState != null)
            {
                m_currentState.Exit();
            }

            m_previousState = m_currentState;
            m_currentState = state;
            m_accumulatedTime = 0;
            m_fixedTickTime = m_currentState.GetTickRate();

            m_currentState.Enter();
        }
        else 
        {
            Debug.LogWarning("NO STATE FOUND WITH THE ID " + id);
        }
    }
    public void RevertState() 
    {
        if (m_previousState == null)
        {
            return;
        }

        m_currentState.Exit();

        IState temp = m_currentState;
        m_currentState = m_previousState;
        m_previousState = temp;
        m_fixedTickTime = Mathf.Max(0f, m_currentState.GetTickRate());

        m_currentState.Enter();
    }   
}
