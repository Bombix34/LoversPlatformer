using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OverwormsGameManager : MonoBehaviour
{
    const float MaxTurnTime = 60;

    private bool gameTerminated = false;
    public VictoryCondition victoryCondition = new DefaultVictoryCondition();
    public TeamManager teamManager;

    private List<object> ordonedHeroes;
    private float turnStartTime;

    public object CurrentHero { get; set; }

    private void Update()
    {
        this.CheckTimeLimit();
    }

    private void CheckTimeLimit()
    {
        if (Time.time <= (turnStartTime + MaxTurnTime))
        {
            this.EndTurn();
        }
    }

    public void EndTurn()
    {
        this.OnEndTurn();
    }

    private void OnStartGame()
    {
        foreach (var team in teamManager.Teams)
        {
            foreach (var hero in team.Heroes)
            {
                this.ordonedHeroes.Add(hero);
            }
        }
        this.ordonedHeroes.OrderBy(a => Guid.NewGuid()).ToList();//Random du piff
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
        this.ordonedHeroes.RemoveAt(0);
        this.ordonedHeroes.Add(this.CurrentHero);
        this.CurrentHero = this.ordonedHeroes[0];
        
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
