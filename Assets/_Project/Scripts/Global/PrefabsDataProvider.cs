

public class PrefabsDataProvider
{
    public PrefabsDataProvider(PrefabsData prefabsData)
    {
        _prefabsData = prefabsData;
    }

    private PrefabsData _prefabsData;

    public string GetPlayerUnitsPrefabsPath() => _prefabsData.PlayerUnitsPrefabsPath;

    public string GetEnemyUnitsPrefabsPath() => _prefabsData.EnemyUnitsPrefabsPath;

    public string GetBackgroundPrefabsPath() => _prefabsData.BackgroundPrefabsPath;

    public string GetEnvironmentPrefabsPath() => _prefabsData.EnvironmentPrefabsPath;
}
