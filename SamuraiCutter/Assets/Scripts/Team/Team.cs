using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Team
{
    public List<HeroManager> Heroes;
    public bool IsAlive 
    {
        get
        {
            return this.IsAnyHeroeAlive();
        }
    }

    private bool IsAnyHeroeAlive()
    {
        return Heroes.Any(h => h.StatsManager.Alive);
    }
}
