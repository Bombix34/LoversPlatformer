using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "OVERWORMS/Skill set d'un hero")]
public class SkillHeroDatabase : ScriptableObject
{

    [Header("des capacités d'un personnage")]
    [Space(-10)]
    [Header("Base de données")]

    [SerializeField]
    private List<SkillManager> m_skills;

    public SkillManager GetSkill(int nb)
    {
        if(nb>= m_skills.Count || nb < 0)
        {
            return null;
        }
        else
        {
            return m_skills[nb];
        }
    }

    public int Size
    {
        get => m_skills.Count;
    }
}
