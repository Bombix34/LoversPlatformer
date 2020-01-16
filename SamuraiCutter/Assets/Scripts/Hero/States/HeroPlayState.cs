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
        m_heroManager.UpdateMovementInput();
        for(int index = 0; index < m_heroManager.Settings.skillsDatabase.Size; ++index)
        {
            UpdateSkillInput(index);
        }
    }


    private void UpdateSkillInput(int inputNb)
    {
        if (m_heroManager.Inputs.GetSkillInputDown(inputNb))
        {
            SkillManager skillUsed = m_heroManager.Settings.skillsDatabase.GetSkill(inputNb);
            m_heroManager.ChangeState(new HeroUseSkillState(m_heroManager, skillUsed, inputNb));
        }
    }

    public override void Exit()
    {

    }
}