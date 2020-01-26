using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTypeEffectTypePlayerDamage : SkillTypeEffectTypeAbstract<SkillTypeEffectConditionDataGameObject>
{
    public int DamagePoint { get; private set; }

    public SkillTypeEffectTypePlayerDamage(int damagePoint)
    {
        this.DamagePoint = damagePoint;
    }
    public override void Activate(SkillTypeEffectConditionDataGameObject conditionData)
    {
        conditionData.GameObject.GetComponent<HeroStatsManager>().RemoveLifePoint(DamagePoint);
    }
}
