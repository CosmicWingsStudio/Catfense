
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

    [field: SerializeField]
    public int RealmIndex { get; private set; }

    [field: SerializeField]
    public int LevelIndex { get; private set; }

    [field: SerializeField, Range(0.01f, 10.0f)]
    public float DifficultyLevel { get; private set; }

    [field: SerializeField]
    public int StartMoney { get; private set; }
    [field: SerializeField]
    public int MoneyPerWave { get; private set; }

    [field: SerializeField, Range(0.01f, 0.9f)]
    public float DifficultyLevelScale { get; private set; }

    public int WavesAmount { get => WavesList.Count; }

    [field: SerializeField]
    public List<LevelWave> WavesList { get; private set; }

    [field: SerializeField]
    public AudioClip[] MusicClips { get; private set; }
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