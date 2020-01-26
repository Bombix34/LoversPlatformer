using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillConditionAbstract
{
    public ISkill Skill { get; private set; }

    public virtual void Init(ISkill skill)
    {
        this.Skill = skill;
    }

    public abstract void Start();

    protected void Activate()
    {
        this.Skill.Activate();
    }
}
