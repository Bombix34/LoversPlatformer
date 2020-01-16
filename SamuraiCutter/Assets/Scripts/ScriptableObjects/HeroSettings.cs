using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "OVERWORMS/Réglages hero")]
public class HeroSettings : ScriptableObject
{
    [Header("Reglages Mouvements")]
    [Range(1f, 50f)]
    public float moveSpeed;

    [Header("Réglages Saut")]
    public float jumpForce = 500f;
    public bool AirControl = false;
    [Range(0f, 1f)]
    public float AirControlMultiplicator = 0.5f;
    [Range(0f, 1f)]
    public float jumpInputTime = 0.35f;
    public bool JumpNuance = false;


    [Header("Reglages Skills")]
    public SkillHeroDatabase skillsDatabase;
}
