using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject backgroundPanel;
    public GameObject teamPanelPrefab;
    [Header("Settings")]
    public MatchSettings matchSettings;
    private List<MatchTeam> matchTeams = new List<MatchTeam>();
    void Start()
    {
        for (int i = 0; i < matchSettings.teamNumber; i++)
        {
            var matchTeam = new MatchTeam($"Team {i}");
            matchTeams.Add(matchTeam);
            var teamPanelGameobject = Instantiate(teamPanelPrefab, backgroundPanel.transform);
            var matchTeamPanel = teamPanelGameobject.GetComponent<MatchTeamPanel>();
            matchTeamPanel.Init(matchTeam, this.matchSettings.heroByTeam);
        }
    }

    public void Play()
    {
        foreach (var matchTeam in matchTeams)
        {
            foreach (var item in matchTeam.Heroes)
            {
                print(item.name);
            }
        }
    }

    public bool AreTeamsValid()
    {
        return matchTeams.Count == matchSettings.teamNumber
            && matchTeams.All(t => IsTeamValid(t));
    }

    public bool IsTeamValid(MatchTeam matchTeam)
    {
        return matchTeam.Heroes.Count == this.matchSettings.heroByTeam
            && matchTeam.Heroes.All(h => h != null);
    }
}

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
public class MatchTeam
{
    public string Name { get; set; }
    public List<GameObject> Heroes { get; set; } = new List<GameObject>();
    public MatchTeam(string name)
    {
        this.Name = name;
    }
}