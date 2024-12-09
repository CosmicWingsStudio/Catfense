
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Create Lvl config/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [field: SerializeField]
    public Sprite BackGround { get; private set; }

    [field: SerializeField]
    public string EnvironmentPrefabName { get; private set; }

    public int WavesAmount { get => WavesList.Count; }

    [field: SerializeField]
    public List<LevelWave> WavesList { get; private set; }
}


[Serializable]
public struct LevelWave
{
    public LevelWave(List<LevelWaveEnemyInfo> Elist)
    {
        EnemiesOnWaveList = Elist;
    }
    public List<LevelWaveEnemyInfo> EnemiesOnWaveList;
}

[Serializable]
public class LevelWaveEnemyInfo
{
    public LevelWaveEnemyInfo(string prefabName, int amount)
    {
        this.PrefabName = prefabName;
        this.Amount = amount;  
    }
    [field: SerializeField] public string PrefabName { get; private set; }
    [field: SerializeField] public int Amount { get; set; }
}