using UnityEngine;

[CreateAssetMenu(fileName = "PrefabsData", menuName = "Scriptable Objects/PrefabsData")]
public class PrefabsData : ScriptableObject
{

    #region InGamePaths
    [field:SerializeField]
    public string PlayerUnitsPrefabsPath { get; private set; }

    [field: SerializeField]
    public string EnemyUnitsPrefabsPath { get; private set; }

    [field: SerializeField]
    public string BackgroundPrefabsPath { get; private set; }

    [field: SerializeField]
    public string EnvironmentPrefabsPath { get; private set; }

    #endregion

    #region InMenuPaths

    #endregion
}
