using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillContainerActivationDirectional : SkillContainerActivationDirectionalForceAbstract
{
    [SerializeField]
    private float force;

    public SkillContainerActivationDirectional()
    {
        this.ActivationData = new SkillContainerActivationDataDirectionalForce
        {
            Force = this.force
        };
    }
    
    public void OnClick(Vector2 from, Vector2 to)
    {
        var direction = (to - from).normalized;
        this.ActivationData.Direction = direction;
        this.Activate();
    }
}
