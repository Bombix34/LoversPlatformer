using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchTeam
{
    public string Name { get; set; }
    public List<GameObject> Heroes { get; set; } = new List<GameObject>();
    public MatchTeam(string name)
    {
        this.Name = name;
    }
}