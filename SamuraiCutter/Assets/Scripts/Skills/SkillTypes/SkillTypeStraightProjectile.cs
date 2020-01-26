using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTypeStraightProjectile : SkillTypeAbstract<SkillContainerActivationDataDirectionalForce>
{
    public GameObject ProjectilePrefab { get; set; }

    public SkillTypeStraightProjectile(List<ISkillTypeEffect> effects) : base(effects)
    {
        
    }

    public override void Activate(SkillContainerActivationDataDirectionalForce activationData)
    {
        this.Target = GameObject.Instantiate(ProjectilePrefab, activationData.StartPosition, Quaternion.identity);
        var rigidbody = this.Target.GetComponent<Rigidbody2D>();
        rigidbody.velocity = activationData.Direction * activationData.Force;

        this.ActivateEffects();
    }
}
