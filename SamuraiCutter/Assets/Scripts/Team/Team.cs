using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Team
{
    public List<HeroManager> Heroes { get; set; }
    public bool IsAlive 
    {
        get
        {
            return this.IsAnyHeroeAlive();
        }
    }

    private bool IsAnyHeroeAlive()//Add check for 
    {
        return true;//Heroes.Any(h => h);
    }
}
