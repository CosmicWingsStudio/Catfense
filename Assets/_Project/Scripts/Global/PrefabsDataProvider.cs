

public class PrefabsDataProvider
{
    public PrefabsDataProvider(PrefabsPathsToFoldersProvider prefabsData)
    {
        _prefabsData = prefabsData;
    }

    private PrefabsPathsToFoldersProvider _prefabsData;

    public string GetPlayerUnitsPrefabsPath() => _prefabsData.PlayerUnitsPrefabsPath;

    public string GetEnemyUnitsPrefabsPath() => _prefabsData.EnemyUnitsPrefabsPath;

    public string GetBackgroundPrefabsPath() => _prefabsData.BackgroundPrefabsPath;

    public string GetEnvironmentPrefabsPath() => _prefabsData.EnvironmentPrefabsPath;
}
