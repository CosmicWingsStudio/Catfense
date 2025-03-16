
using System;

[Serializable]
public class SavedData
{
    public RealmSavedData[] RealmsData;
    public SavedData(RealmSavedData[] realmsData)
    {
        RealmsData = realmsData;
    }
}

[Serializable]
public struct RealmSavedData
{
    public bool[] LevelsData;
    public RealmSavedData(bool[] levelsData)
    {
        LevelsData = levelsData;
    }

}
