using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VictoryCondition
{
    public abstract VictoryData GetVictoryTeam(TeamManager teamManager);
}
