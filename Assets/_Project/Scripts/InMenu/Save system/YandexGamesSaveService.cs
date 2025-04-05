using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using YG;

public class YandexGamesSaveService : ISaveService
{
    readonly RealmsDataHandler RealmsDataHandler;
    private const string PersistentSaveFileName = "/CDSaveData.json";

    public YandexGamesSaveService(RealmsDataHandler realmsDataHandler)
    {
        RealmsDataHandler = realmsDataHandler;
    }

    public SavesYG LoadData()
    {
        if (YandexGame.savesData.SavedData == null)
        {
            SavedData savedData;
            if (RealmsDataHandler.IsInitialized == true)
                savedData = new(RealmsDataHandler.GetDataFromRealmHandlers(), RealmsDataHandler.GetADWatchedStatus());
            else
            {
                RealmsDataHandler.InitializeWithDefaultData();
                savedData = new(RealmsDataHandler.GetDataFromRealmHandlers(), RealmsDataHandler.GetADWatchedStatus());
            }
            YandexGame.savesData.SavedData = savedData;
            return YandexGame.savesData;
        }
        return YandexGame.savesData;
    }

    public void SaveData()
    {
        SavedData savedData = null;

        if (RealmsDataHandler.IsInitialized == true)
            savedData = new(RealmsDataHandler.GetDataFromRealmHandlers(), RealmsDataHandler.GetADWatchedStatus());
        else
        {
            RealmsDataHandler.InitializeWithDefaultData();
            savedData = new(RealmsDataHandler.GetDataFromRealmHandlers(), RealmsDataHandler.GetADWatchedStatus());
        }

        YandexGame.savesData.SavedData = savedData;
        YandexGame.SaveProgress();
        
    }

    public void SaveData(SavesYG savedData)
    {
        YandexGame.savesData.SavedData = savedData.SavedData;
        YandexGame.SaveProgress();
    }

    public void SetData(SavesYG savedData)
    {
        RealmsDataHandler.Initialize(savedData);
    }

    private void CreateSaveFile()
    {
        File.Create(GetSaveDataPath()).Dispose();
    }

    public string GetSaveDataPath()
    {
        return UnityEngine.Application.persistentDataPath + PersistentSaveFileName;
    }
}
