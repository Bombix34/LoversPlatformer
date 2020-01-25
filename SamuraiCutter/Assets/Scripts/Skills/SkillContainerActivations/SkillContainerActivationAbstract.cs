using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillContainerActivationAbstract
{
    public SkillContainer Container { get; set; }
    public SkillContainerActivationDataAbstract ActivationData { get; set; }
    public void Activate()
    {
        this.Container.Activate(this.ActivationData);
    }
}
