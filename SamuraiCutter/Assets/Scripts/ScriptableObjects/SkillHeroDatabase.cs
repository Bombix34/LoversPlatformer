using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "OVERWORMS/Skill set d'un hero")]
public class SkillHeroDatabase : ScriptableObject
{
    public List<SkillManager> skills;
}
