using System.Linq;
using UnityEngine;

public class SkillTypeEffectConditionDetectTerrain : SkillTypeEffectConditionAbstract<SkillTypeEffectConditionDataPosition>
{
    public override void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Terrain")
        {
            var conditionData = new SkillTypeEffectConditionDataPosition
            {
                Position = collision.contacts.First().point
            };
            this.Activate(conditionData);
        }
    }
}
