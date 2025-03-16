
using System.Collections.Generic;
using UnityEngine;

public class RealmLevelsHandler : MonoBehaviour
{
    private bool IsInitialized = false;

    [SerializeField, Tooltip("Put here folder Content in ScrollView")]
    private Transform LevelsFolderForExtraction;

    [Header("Autofills with levels. Do not touch")]
    [SerializeField] private List<RealmLevel> RealmLevels;

    public void Initialize(RealmSavedData realmData)
    {
        if(IsInitialized == false)
        {
            IsInitialized = true;
            FillWithLevelsSlots();
            SetSavedData(realmData);
            ChangeAvailableStatusToPlayForLevels();
        } 
    }

    public void InitializeWithDefaultData()
    {
        if (IsInitialized == false)
        {
            IsInitialized = true;
            FillWithLevelsSlots();
            ChangeAvailableStatusToPlayForLevels();
        }
    }

    private void SetSavedData(RealmSavedData realmData)
    {
        for (int i = 0; i < RealmLevels.Count; i++)
        {
            RealmLevels[i].IsCompleted = realmData.LevelsData[i];
        }
    }

    public RealmSavedData GetSavedData()
    {
        bool[] levelData = new bool[RealmLevels.Count];

        for (int i = 0; i < RealmLevels.Count; i++)
        {        
            levelData[i] = RealmLevels[i].IsCompleted;
        }

        return new RealmSavedData(levelData);
    }

    public List<RealmLevel> GetLevels() => RealmLevels;

    private void FillWithLevelsSlots()
    {
        if (LevelsFolderForExtraction != null)
        {
            if (LevelsFolderForExtraction.childCount != 0)
            {
                for (int i = 0; i < LevelsFolderForExtraction.childCount; i++)
                {
                    if (LevelsFolderForExtraction.GetChild(i).TryGetComponent(out RealmLevel level))
                        RealmLevels.Add(level);
                }
            }
            else
                Debug.LogError("There's nothing in LevelsFolder");

        }
        else
            Debug.LogError("No LevelsFolder given");
    }

    private void ChangeAvailableStatusToPlayForLevels()
    {
        for (int i = 0; i < RealmLevels.Count; i++)
        {
            if (i == 0)
                RealmLevels[i].MakeLevelAvailable();
            else if (RealmLevels[i - 1].IsCompleted || RealmLevels[i].IsCompleted)
                RealmLevels[i].MakeLevelAvailable();
        }
    }
}
