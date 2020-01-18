using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MatchSettings
{
    [Range(10, 120)]
    public int turnTime = 10;
    [Range(2, 8)]
    public int teamNumber = 2;
    [Range(1, 10)]
    public int heroByTeam = 1;
}
