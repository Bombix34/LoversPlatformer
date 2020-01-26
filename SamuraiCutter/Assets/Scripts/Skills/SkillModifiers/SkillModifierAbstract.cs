using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillModifierAbstract
{
    private SkillContainerActivationDataAbstract ActivationData { get; set; }

    public void Apply(SkillContainerActivationDataAbstract activationData)
    {
        this.ActivationData = activationData;
        this.ApplyModifier();
    }

    protected abstract void ApplyModifier();
}
