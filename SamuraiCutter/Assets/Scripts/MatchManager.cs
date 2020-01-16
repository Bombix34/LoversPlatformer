using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    public MatchSettings matchSettings;
    public List<GameObject> heroes;
    public List<MatchTeam> teams = new List<MatchTeam>();
    
    void Start()
    {
        for (int i = 0; i < matchSettings.teamNumber; i++)
        {
            teams.Add(new MatchTeam());
        }
    }

    public bool AreTeamsValid()
    {
        return teams.Count == matchSettings.teamNumber
            && teams.All(t => IsTeamValid(t));
    }

    public bool IsTeamValid(MatchTeam matchTeam)
    {
        return matchTeam.heroes.Count == this.matchSettings.heroByTeam;
    }
}

[System.Serializable]
public class MatchSettings
{
    [Range(10, 120)]
    public int turnTime = 10;
    [Range(2, 4)]
    public int teamNumber = 2;
    [Range(1, 10)]
    public int heroByTeam = 1;
}
public class MatchTeam
{
    public List<GameObject> heroes { get; set; } = new List<GameObject>();
}