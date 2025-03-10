using UnityEngine;

[CreateAssetMenu(fileName = "RestartLevelDataSaver", menuName = "Scriptable Objects/RestartLevelDataSaver")]
public class RestartLevelDataSaver : ScriptableObject
{
    public bool OnRestart { get; set; }

    public int AdditionalMoney { get; set; }

    public LevelConfig LevelConfig { get; set; }


    public void SetData(int additionalMoney, LevelConfig lc)
    {
        OnRestart = true;
        AdditionalMoney = additionalMoney;
        LevelConfig = lc;
    }

    public void SetDefaultData()
    {
        OnRestart = false;
        AdditionalMoney = 0;
        LevelConfig = null;
    }
}
