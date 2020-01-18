using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OverwormsGameManager : MonoBehaviour
{
    public float MaxTurnTime = 10;

    private bool gameStarted = false;
    private bool gameTerminated = false;
    public VictoryCondition victoryCondition = new DefaultVictoryCondition();
    private TeamManager teamManager;

    private List<HeroManager> ordonedHeroes;
    private float turnStartTime;

    public HeroManager CurrentHero { get; set; }

    private void Update()
    {
        if (!gameStarted)
        {
            return;
        }
        this.CheckTimeLimit();
    }

    private void CheckTimeLimit()
    {
        if (Time.time >= (turnStartTime + MaxTurnTime))
        {
            this.EndTurn();
        }
    }

    public void EndTurn()
    {
        this.OnEndTurn();
    }

    public void StartGame()
    {
        this.teamManager = GetComponent<TeamManager>();
        this.ordonedHeroes = new List<HeroManager>();
        foreach (var team in teamManager.Teams)
        {
            foreach (var hero in team.Heroes)
            {
                this.ordonedHeroes.Add(hero);
            }
        }
        this.ordonedHeroes = this.ordonedHeroes.OrderBy(a => Guid.NewGuid()).ToList();//Random du piff
        this.NextTurn();

        this.gameStarted = true;
    }

    private void OnEndTurn()
    {
        if (this.gameTerminated || this.CheckVictory())
        {
            return;
        }
        this.NextTurn();
    }

    private void NextTurn()
    {
        this.turnStartTime = Time.time;
        this.NextHero();
    }

    private void NextHero()
    {
        if(this.CurrentHero != null)
        {
            this.CurrentHero.ChangeState(new HeroWaitState(this.CurrentHero));
            this.ordonedHeroes.RemoveAt(0);
            this.ordonedHeroes.Add(this.CurrentHero);
        }

        this.CurrentHero = this.ordonedHeroes[0];
        Debug.Log(CurrentHero);
        this.CurrentHero.ChangeState(new HeroPlayState(this.CurrentHero));

    }

    private bool CheckVictory()
    {
        var victoryData = this.victoryCondition.GetVictoryTeam(this.teamManager);
        switch (victoryData.State)
        {
            case VictoryData.VictoryState.UNDEFINED:
                break;
            case VictoryData.VictoryState.DRAW:
                Debug.Log("draw");
                this.gameTerminated = true;
                break;
            case VictoryData.VictoryState.VICTORY:
                Debug.Log("victory");
                this.gameTerminated = true;
                break;
            case VictoryData.VictoryState.DEFEAT:
                Debug.Log("defeat");
                this.gameTerminated = true;
                break;
            default:
                break;
        }

        return this.gameTerminated;
    }
}
