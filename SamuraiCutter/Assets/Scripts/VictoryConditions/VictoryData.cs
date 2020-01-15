using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct VictoryData
{
	public enum VictoryState
	{
		UNDEFINED = 0,
		DRAW = 1,
		VICTORY = 2,
		DEFEAT = 3
	}

	public VictoryState State { get; }
	public List<Team> Teams { get; }

	public VictoryData(VictoryState victoryState, List<Team> teams)
	{
		this.State = victoryState;
		this.Teams = teams;
	}
}
