using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MatchTeam
{
    [SerializeField]
    private string name;
    [SerializeField]
    private List<GameObject> heroes = new List<GameObject>();

    public string Name { get => name; set => name = value; }
    public List<GameObject> Heroes { get => heroes; set => heroes = value; }
    public MatchTeam(string name)
    {
        this.Name = name;
    }
}