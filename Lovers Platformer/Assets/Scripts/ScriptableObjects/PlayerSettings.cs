using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Lovers/Player Settings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Reglages Mouvements")]
    [Range(1f,50f)]
    public float moveSpeed;
}
