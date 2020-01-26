using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillTypeEffectTypeAbstract<TConditionData> where TConditionData : SkillTypeEffectConditionDataAbstract
{
    public SkillTypeEffect<TConditionData> SkillTypeEffect { get; private set; }
    public virtual void Init(SkillTypeEffect<TConditionData> skillTypeEffect)
    {
        this.SkillTypeEffect = skillTypeEffect;
    }

    public abstract void Activate(TConditionData conditionData);
}
