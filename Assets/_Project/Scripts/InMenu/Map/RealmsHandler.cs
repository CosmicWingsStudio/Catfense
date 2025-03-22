using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;

public class RealmsHandler : MonoBehaviour
{
    [SerializeField, Tooltip("Put here a folder that contains Realms folders")]
    private Transform RealmsFolderForExtraction;

    [Header("Autofills with realms. Do not touch")]
    [SerializeField] private List<Realm> Realms;

    private RealmADWatchedSavedData _loadedData;

    public void Initialize()
    {
        FillWithRealms();
        FillWithLoadedData();
        ProcessSavedLevelsData();
        ChangeAvailableStatusForRealm();
    }

    private void FillWithRealms()
    {
        if (RealmsFolderForExtraction != null)
        {
            if (RealmsFolderForExtraction.childCount != 0)
            {
                for (int i = 0; i < RealmsFolderForExtraction.childCount; i++)
                {
                    if (RealmsFolderForExtraction.GetChild(i).TryGetComponent(out Realm realm))
                        Realms.Add(realm);
                }
            }
            else
                Debug.LogError("There's nothing in RealmsFolder");

        }
        else
            Debug.LogError("No RealmsFolder given");
    }

    private void ProcessSavedLevelsData()
    {
        for (int i = 0; i < Realms.Count; i++)
        {
            var levels = Realms[i].realmLevelsHandler.GetLevels();
            if (levels[levels.Count - 1].IsCompleted == true)
                Realms[i].IsCompleted = true;

        }
    }

    private void ChangeAvailableStatusForRealm()
    {
        for (int i = 0; i < Realms.Count; i++)
        {
            if (i == 0)
                Realms[i].MakeRealmAvailable();
            else if(i == 5)
            {
                if (Realms[2].IsAvaliable)
                    Realms[i].MakeRealmAvailable();
            }
            else if (Realms[i-1].IsCompleted)
                Realms[i].MakeRealmAvailable();
        }
    }

    private void FillWithLoadedData()
    {
        for (int i = 0; i < Realms.Count; i++)
        {
            Realms[i].IsADWatched = _loadedData.RealmsADWatchedData[i];
        }
    }

    public List<Realm> GetRealms()
    {
        return Realms;
    }

    public void SetData(RealmADWatchedSavedData data)
    {
        _loadedData = data;
    }
}
