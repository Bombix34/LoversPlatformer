using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{ 
    [SerializeField] PlayerManager player_1;
    [SerializeField] PlayerManager player_2;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public PlayerManager GetPlayer1()
    {
        return player_1;
    }

    public PlayerManager GetPlayer2()
    {
        return player_2;
    }
}
