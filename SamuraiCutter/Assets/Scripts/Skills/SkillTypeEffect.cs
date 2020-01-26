using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillTypeEffect
{
    ISkillType SkillType { get; }
    void Init(ISkillType skillType);
    void Start();
}

//Ne par supprimer, obligatoire avec les histoires de generic
//public abstract class SkillTypeEffect
//{
//    public SkillTypeEffectConditionAbstract<SkillTypeEffectConditionDataAbstract> Condition { get; private set; }
//    public SkillTypeEffectTypeAbstract<SkillTypeEffectConditionDataAbstract> Type { get; private set; }
//    public ISkillType SkillType { get; protected set; }
//}

public class SkillTypeEffect<TConditionData> : ISkillTypeEffect where TConditionData : SkillTypeEffectConditionDataAbstract
{
    public ISkillType SkillType { get; protected set; }
    public SkillTypeEffectConditionAbstract<TConditionData> Condition { get; private set; }
    public SkillTypeEffectTypeAbstract<TConditionData> Type { get; private set; }

    public SkillTypeEffect(SkillTypeEffectConditionAbstract<TConditionData> condition, SkillTypeEffectTypeAbstract<TConditionData> type)
    {
        this.Condition = condition;
        this.Type = type;
    }

    public void Init(ISkillType skillType)
    {
        this.SkillType = skillType;
        this.Condition.Init(this);
        this.Type.Init(this);
    }

    public void Start()
    {

    }
    public void Activate(TConditionData conditionData)
    {
        this.Type.Activate(conditionData);
    }
}
