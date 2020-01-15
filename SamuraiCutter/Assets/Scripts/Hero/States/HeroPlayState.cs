using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPlayState : HeroState
{

    public HeroPlayState(HeroManager hero)
    {
        m_stateName = "HERO_PLAY_STATE";
        m_objectManager = hero;
        m_heroManager = hero;
    }

    public override void Enter()
    {

    }

    public override void Execute()
    {

    }

    public override void Exit()
    {

    }
}