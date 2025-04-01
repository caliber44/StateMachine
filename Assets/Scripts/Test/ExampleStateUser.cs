using UnityEngine;

public class ExampleStateUser : MonoBehaviour
{
    private StateMachine m_stateMachine;

    private bool m_testPause = false;

    void Start()
    {
        m_stateMachine = GetComponent<StateMachine>();

        m_stateMachine.AddState(new IdleState());
        m_stateMachine.AddState(new AttackState());
        m_stateMachine.AddState(new RunState());
        m_stateMachine.AddState(new RunningAttackState());

        m_stateMachine.ChangeState((int)TestStateID.IDLE);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            m_testPause = !m_testPause;

            if (m_testPause)
            {
                m_stateMachine.Pause();
            }
            else
            {
                m_stateMachine.Play();
            }
        }
    }
}
