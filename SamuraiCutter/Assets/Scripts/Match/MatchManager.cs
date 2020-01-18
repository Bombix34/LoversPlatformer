using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject backgroundPanel;
    public GameObject teamPanelPrefab;
    [Header("Settings")]
    public MatchSettings matchSettings;
    private List<MatchTeam> matchTeams;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        this.matchTeams = new List<MatchTeam>();
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
        print(this.matchTeams.Count);
        if (!this.AreTeamsValid())
        {
            return;
        }
        SceneManager.sceneLoaded += this.OnGameSceneLoaded;
        SceneManager.LoadScene("GameScene");
    }


    void OnGameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var teamManager = FindObjectOfType<TeamManager>();
        teamManager.Init(matchTeams);
        var gameManager = FindObjectOfType<OverwormsGameManager>();
        gameManager.MaxTurnTime = this.matchSettings.turnTime;
        gameManager.StartGame();
        SceneManager.sceneLoaded -= this.OnGameSceneLoaded;
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