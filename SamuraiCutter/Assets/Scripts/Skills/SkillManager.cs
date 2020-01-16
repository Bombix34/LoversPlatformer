using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SkillManager
{
    protected HeroManager m_hero;

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

    #endregion
}
