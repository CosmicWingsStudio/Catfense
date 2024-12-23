using UnityEngine;

public interface ISaveService
{
    SavedData LoadData();
    void SaveData(SavedData savedData) { }
    void SaveData() { }
    void SetData(SavedData savedData);
    string GetSaveDataPath();
}
