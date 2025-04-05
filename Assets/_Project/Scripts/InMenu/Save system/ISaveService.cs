using UnityEngine;
using YG;

public interface ISaveService
{
    SavesYG LoadData();
    void SaveData(SavesYG savedData) { }
    void SaveData() { }
    void SetData(SavesYG savedData);
    string GetSaveDataPath();
}
