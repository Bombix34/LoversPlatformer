using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillContainerActivationDirectionalForceAbstract : SkillContainerActivationAbstract<SkillContainerActivationDataDirectionalForce>
{
    public SkillContainerActivationDirectionalForceAbstract(List<Skill<SkillContainerActivationDataDirectionalForce>> skills) : base(skills)
    {
    }
}
