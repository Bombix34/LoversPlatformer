using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DefaultVictoryCondition : VictoryCondition
{
    public override VictoryData GetVictoryTeam(TeamManager teamManager)
    {
        var aliveTeams = teamManager.Teams.Where(t => t.IsAlive).ToList();
        switch (aliveTeams.Count)
        {
            case (0):
                return new VictoryData(VictoryData.VictoryState.DRAW, aliveTeams);
            case (1):
                return new VictoryData(VictoryData.VictoryState.VICTORY, aliveTeams);
            default:
                return new VictoryData(VictoryData.VictoryState.UNDEFINED, aliveTeams);
        }
    }
}
