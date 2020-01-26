using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTypeEffectConditionAbstract<TConditionData> where TConditionData : SkillTypeEffectConditionDataAbstract
{
    public SkillTypeEffect<TConditionData> SkillTypeEffect { get; private set; }
    public TConditionData ConditionData { get; set; }
    public virtual void Init(SkillTypeEffect<TConditionData> skillTypeEffect)
    {
        this.SkillTypeEffect = skillTypeEffect;
    }

    public virtual void OnCollisionEnter2D(Collision2D col) { }
    protected void Activate(SkillTypeEffectConditionDataAbstract conditionData)
    {
        this.SkillTypeEffect.Activate(this.ConditionData);
    }
}
