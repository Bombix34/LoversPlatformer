using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTest : MonoBehaviour
{
    public GameObject prefab;
    public SkillContainer SkillContainer { get; set; }

    private void Start()
    {
        this.SkillContainer = GetComponent<SkillContainer>();

        var skills = new List<Skill<SkillContainerActivationDataDirectionalForce>>
        {
            new Skill<SkillContainerActivationDataDirectionalForce>
            (
                new SkillConditionDefault(),
                new List<SkillModifierAbstract>() { new SkillModifierDefault() },
                new SkillTypeStraightProjectile(
                    new List<ISkillTypeEffect>()
                    {
                        new SkillTypeEffect<SkillTypeEffectConditionDataGameObject>
                        (
                            new SkillTypeEffectConditionDetectPlayer(),
                            new SkillTypeEffectTypePlayerDamage(10)
                        )
                    })
                {
                    ProjectilePrefab = prefab
                }
            ),
            new Skill<SkillContainerActivationDataDirectionalForce>
            (
                new SkillConditionTimer(500),
                new List<SkillModifierAbstract>() { new SkillModifierDefault() },
                new SkillTypeStraightProjectile(
                    new List<ISkillTypeEffect>()
                    {
                        new SkillTypeEffect<SkillTypeEffectConditionDataGameObject>
                        (
                            new SkillTypeEffectConditionDetectPlayer(),
                            new SkillTypeEffectTypePlayerDamage(10)
                        )
                    })
                {
                    ProjectilePrefab = prefab
                }
            ),
            new Skill<SkillContainerActivationDataDirectionalForce>
            (
                new SkillConditionTimer(1000),
                new List<SkillModifierAbstract>() { new SkillModifierDefault() },
                new SkillTypeStraightProjectile(
                    new List<ISkillTypeEffect>()
                    {
                        new SkillTypeEffect<SkillTypeEffectConditionDataGameObject>
                        (
                            new SkillTypeEffectConditionDetectPlayer(),
                            new SkillTypeEffectTypePlayerDamage(10)
                        )
                    })
                {
                    ProjectilePrefab = prefab
                }
            )
        };
        var containerActivation = new SkillContainerActivationDirectional(skills, Vector2.zero, 10);
        this.SkillContainer.Activation = containerActivation;

        this.SkillContainer.Init();
        containerActivation.OnClick(Vector2.one);
    }
}
