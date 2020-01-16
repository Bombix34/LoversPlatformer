using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroWaitState : HeroState
{
    public HeroWaitState(HeroManager hero)
    {
        m_stateName = "HERO_WAIT_STATE";
        m_objectManager = hero;
        m_heroManager = hero;
    }

    public override void Enter()
    {
        m_heroManager.Body.velocity = Vector2.zero;
    }

    public override void Execute()
    {

    }

    public override void Exit()
    {

    }
}