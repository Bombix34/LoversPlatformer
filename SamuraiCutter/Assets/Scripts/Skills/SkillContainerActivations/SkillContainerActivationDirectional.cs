using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillContainerActivationDirectional : SkillContainerActivationDirectionalForceAbstract
{
    [SerializeField]
    private float force;

    public SkillContainerActivationDirectional(List<Skill<SkillContainerActivationDataDirectionalForce>> skills, Vector2 startPosition) : base(skills)
    {
        this.ActivationData = new SkillContainerActivationDataDirectionalForce
        {
            StartPosition = startPosition,
            Force = this.force
        };
    }

    public SkillContainerActivationDirectional(List<Skill<SkillContainerActivationDataDirectionalForce>> skills, Vector2 startPosition, float force) : base(skills)
    {
        this.ActivationData = new SkillContainerActivationDataDirectionalForce
        {
            StartPosition = startPosition,
            Force = force
        };
    }

    public void OnClick(Vector2 position)
    {
        var direction = (position - this.ActivationData.StartPosition).normalized;
        this.ActivationData.Direction = direction;
        this.Activate();
    }
}
