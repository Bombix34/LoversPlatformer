using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : ObjectManager
{
    protected PlayerInputManager m_playerInput;

    protected void Start()
    {
        ChangeState(new HeroPlayState(this));
    }

    protected void Update()
    {
        m_currentState.Execute();
    }
}
