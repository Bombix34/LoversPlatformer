using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillContainer : MonoBehaviour
{
    public ISkillContainerActivationAbstract Activation { get; set; }
    //public List<ISkill> Skills { get; set; }

    public void Init()
    {
        this.Activation.Init(this);

        //foreach (var skill in Skills)
        //{
        //    skill.Init(this);
        //}
    }

    public void Activate()
    {

        //foreach (var skill in Skills)
        //{
        //    skill.Prepare();
        //}
    }
}
