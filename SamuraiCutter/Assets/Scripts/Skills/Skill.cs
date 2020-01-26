using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    //SkillConditionAbstract Condition { get; }
    //List<SkillModifierAbstract> Modifiers { get; }

    void Activate();
    //void Init(SkillContainer container);
    //void Prepare();
    //void Terminate();
}

public class Skill<TActivationData>: ISkill where TActivationData : SkillContainerActivationDataAbstract
{
    public SkillContainerActivationAbstract<TActivationData> Activation { get; private set; }
    public SkillConditionAbstract Condition { get; private set; }
    public List<SkillModifierAbstract> Modifiers { get; private set; }
    public SkillTypeAbstract<TActivationData> Type { get; private set; }

    public Skill(SkillConditionAbstract condition, List<SkillModifierAbstract> modifiers, SkillTypeAbstract<TActivationData> type)
    {
        this.Condition = condition;
        this.Modifiers = modifiers;
        this.Type = type;
    }

    public void Init(SkillContainerActivationAbstract<TActivationData> activation)
    {
        this.Activation = activation;
        this.Condition.Init(this);
        this.Type.Init(this);
    }

    public void Prepare()
    {
        foreach (var modifier in this.Modifiers)
        {
            modifier.Apply(Activation.ActivationData);
        }
        this.Condition.Start();
    }

    public void Activate()
    {
        this.Type.Activate(Activation.ActivationData);
    }

    public void Terminate()
    {

    }
}
