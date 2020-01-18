using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager: MonoBehaviour
{
    public List<Team> Teams;
    public void Init(List<MatchTeam> matchTeams)//COULEUR DEGOLASSE
    {
        foreach (var matchTeam in matchTeams)
        {
            var team = new Team();
            Teams.Add(team);
            foreach (var heroPrefab in matchTeam.Heroes)
            {
                var heroGameObject = Instantiate(heroPrefab);
                var heroManager = heroGameObject.GetComponent<HeroManager>();
                team.Heroes.Add(heroManager);
            }
        }
        this.SetColors();
    }

    private void SetColors()
    {

        var colorArray = new List<Color>()
        {
            Color.blue,
            Color.red,
            Color.green,
            Color.yellow,
            Color.white,
            Color.black,
            Color.magenta,
            Color.cyan
        };
        int teamIndex = 0;
        foreach (var team in Teams)
        {
            foreach (var hero in team.Heroes)
            {
                if (teamIndex >= colorArray.Count)
                {
                    return;
                }
                hero.GetComponentInChildren<SpriteRenderer>().color = colorArray[teamIndex];
            }
            teamIndex++;
        }
    }
}
