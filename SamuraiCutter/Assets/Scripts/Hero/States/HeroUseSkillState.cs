using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroUseSkillState : HeroState
{

    private SkillManager m_skill;
    private int m_skillInputNb;

    public HeroUseSkillState(HeroManager hero)
    {
        m_stateName = "HERO_USE_SKILL_STATE";
        m_objectManager = hero;
        m_heroManager = hero;
    }

    public HeroUseSkillState(HeroManager hero, SkillManager skill, int skillInputNb)
    {
        m_stateName = "HERO_USE_SKILL_STATE";
        m_objectManager = hero;
        m_heroManager = hero;
        m_skill = skill;
        m_skillInputNb = skillInputNb;
    }

    public override void Enter()
    {
        m_skill.Hero = m_heroManager;
        m_skill.UseSkill();
    }

    public override void Execute()
    {
        m_skill.UpdateSkill();
        if(m_skill.IsSkillEnded)
        {
            Exit();
        }

    }

    public override void Exit()
    {

    }
}