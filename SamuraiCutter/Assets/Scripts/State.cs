using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    protected string m_stateName;
    protected ObjectManager m_objectManager;

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();

    //public abstract void OnReceiveMessage(Telegram message);

}
