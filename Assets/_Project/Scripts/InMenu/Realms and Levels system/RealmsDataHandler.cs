
using UnityEngine;
using YG;
using Zenject;

public class RealmsDataHandler : MonoBehaviour
{
    [SerializeField] private RealmLevelsHandler[] RealmHandlers;
    [Inject] readonly ISaveService saveService;
    [Inject] readonly RealmsHandler realmsHandler;

    public bool IsInitialized { get; private set; } = false;

    public void Initialize(SavesYG savedData)
    {
        for (int i = 0; i < RealmHandlers.Length; i++)
        {
            RealmHandlers[i].Initialize(savedData.SavedData.RealmsData[i]);
            
        }

        realmsHandler.SetData(savedData.SavedData.RealmsADWatchedData);
        IsInitialized = true;
    }

    public void InitializeWithDefaultData()
    {
        for (int i = 0; i < RealmHandlers.Length; i++)
        {
            RealmHandlers[i].InitializeWithDefaultData();
        }

        IsInitialized = true;
    }

    public RealmSavedData[] GetDataFromRealmHandlers()
    {
        RealmSavedData[] realmSavedData = new RealmSavedData[RealmHandlers.Length];

        for (int i = 0; i < RealmHandlers.Length; i++)
        {
            realmSavedData[i] = RealmHandlers[i].GetSavedData();
        }

        return realmSavedData;
    }

    public RealmADWatchedSavedData GetADWatchedStatus()
    {
        var realms = realmsHandler.GetRealms();
        bool[] statuses = new bool[realms.Count];

        if(realms.Count == 0)
        {
            return new RealmADWatchedSavedData(new bool [6] { false, false, false, false, false, false });
        }

        for (int i = 0; i < realms.Count; i++)
        {
            statuses[i] = realms[i].IsADWatched;
        }

        return new RealmADWatchedSavedData(statuses);
    }
}
