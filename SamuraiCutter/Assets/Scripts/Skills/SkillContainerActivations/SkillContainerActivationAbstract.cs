using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillContainerActivationAbstract
{
    void Init(SkillContainer container);
}

public abstract class SkillContainerActivationAbstract<TActivationData> : ISkillContainerActivationAbstract where TActivationData : SkillContainerActivationDataAbstract
{
    public SkillContainer Container { get; private set; }
    public TActivationData ActivationData { get; protected set; }
    public List<Skill<TActivationData>> Skills { get; private set; }
    public SkillContainerActivationAbstract(List<Skill<TActivationData>> skills)
    {
        this.Skills = skills;
    }
    public virtual void Init(SkillContainer container)
    {
        this.Container = container;
        foreach (var skill in Skills)
        {
            skill.Init(this);
        }
    }

    public void Activate()
    {

        this.Container.Activate();
        foreach (var skill in Skills)
        {
            skill.Prepare();
        }
    }
}
