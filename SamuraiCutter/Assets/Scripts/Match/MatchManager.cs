using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour
{
    public bool useUI = true;
    [Header("UI")]
    public GameObject backgroundPanel;
    public GameObject teamPanelPrefab;
    [Header("Settings")]
    public MatchSettings matchSettings;
    [SerializeField]
    private List<MatchTeam> matchTeams;
    void Start()
    {
        if (this.useUI)
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
        else
        {
            this.InitGame();
        }
    }

    public void Play()
    {
        if (!this.AreTeamsValid())
        {
            return;
        }
        SceneManager.sceneLoaded += this.OnGameSceneLoaded;
        SceneManager.LoadScene("GameScene");
    }


    private void OnGameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.InitGame();
        SceneManager.sceneLoaded -= this.OnGameSceneLoaded;
    }

    private void InitGame()
    {
        var teamManager = FindObjectOfType<TeamManager>();
        teamManager.Init(matchTeams);
        var gameManager = FindObjectOfType<OverwormsGameManager>();
        gameManager.MaxTurnTime = this.matchSettings.turnTime;
        gameManager.StartGame();
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