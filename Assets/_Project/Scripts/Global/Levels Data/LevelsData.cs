
using System;
using UnityEngine;

public class LevelsData
{
    private LevelData[] Realm1;
    private LevelData[] Realm2;

}

[Serializable]
public struct LevelData
{
    LevelData(bool status, int number)
    {
        IsCompleted = status;
        NumberOfLevel = number;
    }

    public bool IsCompleted;
    public int NumberOfLevel;
}

