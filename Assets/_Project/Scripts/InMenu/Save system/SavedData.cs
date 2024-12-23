
using System;

[Serializable]
public class SavedData
{
    public string Username { get; set; }
    public RealmSavedData[] RealmsData;

    public SavedData(RealmSavedData[] realmsData, string username = "QWERTY")
    {
        Username = username;
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
