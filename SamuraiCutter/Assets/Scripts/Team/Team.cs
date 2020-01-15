using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    public List<object> Heroes { get; set; }
    public bool IsAlive 
    {
        get
        {
            return this.IsAnyHeroeAlive();
        }
    }

    private bool IsAnyHeroeAlive()//Add check for 
    {
        return true;
    }
}
