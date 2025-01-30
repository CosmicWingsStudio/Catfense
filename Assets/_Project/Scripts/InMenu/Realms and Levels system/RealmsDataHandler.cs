
using UnityEngine;
using Zenject;

public class RealmsDataHandler : MonoBehaviour
{
    [SerializeField] private RealmLevelsHandler[] RealmHandlers;
    [Inject] readonly ISaveService saveService;
    public bool IsInitialized { get; private set; } = false;

    public void Initialize(SavedData savedData)
    {
        for (int i = 0; i < RealmHandlers.Length; i++)
        {
            RealmHandlers[i].Initialize(savedData.RealmsData[i]);
        }

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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            saveService.SaveData();
        }
        
    }
}
