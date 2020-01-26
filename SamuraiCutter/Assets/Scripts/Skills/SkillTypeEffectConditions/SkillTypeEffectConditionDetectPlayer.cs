using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTypeEffectConditionDetectPlayer : SkillTypeEffectConditionAbstract<SkillTypeEffectConditionDataGameObject>
{
    public override void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Player")
        {
            var data = new SkillTypeEffectConditionDataGameObject
            {

            };
            this.Activate(data);
        }
    }
}
