using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillType
{
    //List<SkillTypeEffect> Effects { get; set; }
    //Skill Skill { get; }
    //Func<TActivationData, string> Test { get; set; }
    //void Activate(SkillContainerActivationDataAbstract activationData);
    //void Init(Skill skill);
    void Init(ISkill skill);
}

public abstract class SkillTypeAbstract<TActivationData> : ISkillType where TActivationData : SkillContainerActivationDataAbstract
{
    public GameObject Target { get; protected set; }
    public List<ISkillTypeEffect> Effects { get; set; }
    public ISkill Skill { get; private set; }
    public SkillTypeAbstract(List<ISkillTypeEffect> effects)
    {
        this.Effects = effects;
    }

    public abstract void Activate(TActivationData activationData);
    protected void ActivateEffects()
    {
        foreach (var effect in this.Effects)
        {
            effect.Start();
        }
    }
    public virtual void Init(ISkill skill)
    {
        this.Skill = skill;
        foreach (var effect in this.Effects)
        {
            effect.Init(this);
        }
    }
}
