using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager: MonoBehaviour
{
    public List<Team> Teams;
    void Start()//COULEUR DEGOLASSE
    {
        var colorArray = new List<Color>()
        {
            Color.blue,
            Color.red,
            Color.green,
            Color.yellow
        };
        int teamIndex = 0;
        foreach (var team in Teams)
        {
            foreach (var hero in team.Heroes)
            {
                if(teamIndex >= colorArray.Count)
                {
                    return;
                }
                hero.GetComponentInChildren<SpriteRenderer>().color = colorArray[teamIndex];
            }
            teamIndex++;
        }
    }
}
