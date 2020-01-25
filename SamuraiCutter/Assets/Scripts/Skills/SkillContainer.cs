using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillContainer : MonoBehaviour
{
    public SkillContainerActivationAbstract Activation { get; set; }
    public List<SkillAbstract> Skills { get; set; }

    public void Activate(SkillContainerActivationDataAbstract activationData)
    {

    }
}
