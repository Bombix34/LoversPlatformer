using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SkillManager : ScriptableObject
{
    protected HeroManager m_hero;
    protected bool m_endSkill = false;

    public abstract void UseSkill();
    public abstract void UpdateSkill();

    #region ACCESSOR

    public HeroManager Hero
    {
        set
        {
            m_hero = value;
        }
    }

    public bool IsSkillEnded
    {
        get => m_endSkill;
    }

    #endregion
}
