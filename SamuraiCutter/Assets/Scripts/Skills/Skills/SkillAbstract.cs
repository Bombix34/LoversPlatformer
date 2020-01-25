using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAbstract
{
    public SkillConditionAbstract Conditions { get; set; }
    public List<SkillModifierAbstract> Modifiers { get; set; }
    public SkillTypeAbstract Type { get; set; }
}
