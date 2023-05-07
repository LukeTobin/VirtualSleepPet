using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SleepData
{
    public string ID = "SLEEP_DATA";

    public Vector2[] Days = new Vector2[7];

    public List<Block> Routines = new List<Block>();

    public int rewardChapter = 0;
    public List<LevelState> levelStates = new List<LevelState>(4){LevelState.Empty, LevelState.Empty, LevelState.Empty, LevelState.Empty};
}

public enum LevelState{
    Empty,
    Leveled,
    Stored
}