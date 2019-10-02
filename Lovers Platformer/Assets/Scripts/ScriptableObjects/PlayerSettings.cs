using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Lovers/Player Settings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Reglages Mouvements")]
    [Range(1f,50f)]
    public float moveSpeed;

    [Header("Réglages Saut")]
   // [Range(1f, 50f)]
    public float jumpForce = 500f;
    public bool AirControl = false;
    [Range(0f, 1f)]
    public float AirControlMultiplicator = 0.5f;
    [Range(0f, 1f)]
    public float jumpInputTime = 0.35f;
    public bool JumpNuance = false;

    [Header("Réglages Dash")]
    public float DashForce = 500f;
    public float DashDuration = 0.3f;
    [Range(0f, 5f)]
    public float DashCooldown = 1f;
}
