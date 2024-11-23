
using System.Collections.Generic;
using UnityEngine;

public class RealmLevelsHandler : MonoBehaviour
{
    [SerializeField, Tooltip("Put here folder Content in ScrollView")]
    private Transform LevelsFolderForExtraction;

    [Header("Autofills with levels. Do not touch")]
    [SerializeField] private List<RealmLevel> RealmLevels;

    private void Start()
    {
        FillWithLevelsSlots();
        ProcessSavedLevelsData();
        ChangeAvailableStatusToPlayForLevels();
    }

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

    private void ProcessSavedLevelsData()
    {
        //LevelsData
        //делаем тут какие уровни у нас Комплитед, а какие нет на основе сэйв дэйты
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
