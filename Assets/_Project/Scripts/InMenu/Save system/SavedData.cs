
using System;

[Serializable]
public class SavedData
{
    public RealmSavedData[] RealmsData;
    public RealmADWatchedSavedData RealmsADWatchedData;
    public SavedData(RealmSavedData[] realmsData, RealmADWatchedSavedData realmsADWatchedData)
    {
        RealmsData = realmsData;
        RealmsADWatchedData = realmsADWatchedData;
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

[Serializable]
public struct RealmADWatchedSavedData
{
    public bool[] RealmsADWatchedData;
    public RealmADWatchedSavedData(bool[] adWatchedData)
    {
        RealmsADWatchedData = adWatchedData;
    }

}
