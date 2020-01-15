using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectManager : MonoBehaviour
{
    protected State m_currentState;

    /// <summary>
    /// servira peut-etre plus tard
    /// </summary>
    //public abstract void ReceiveMessage();
    
    public void ChangeState(State newState)
    {
        if(m_currentState != null)
        {
            m_currentState.Exit();
        }
        m_currentState = newState;
        m_currentState.Enter();
    }
}
