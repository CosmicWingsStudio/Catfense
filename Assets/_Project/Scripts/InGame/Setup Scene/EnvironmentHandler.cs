
using UnityEngine;
using Zenject;

public class EnvironmentHandler : MonoBehaviour
{
    [Inject] PrefabsPathsToFoldersProvider _prefabsData;

    private EnvironmentContainerHandler _eContainerHandler;

    public void SetEnvironment(string PrefabName)
    {
        _eContainerHandler = Instantiate(Resources.Load<EnvironmentContainerHandler>(_prefabsData.EnvironmentPrefabsPath + PrefabName), transform);  
    }

    public Transform GetEnemySpawnPoint() => _eContainerHandler.EnemySpawnPoint;

    public EnvironmentContainerHandler GetEnvironmentContainer() => _eContainerHandler;


}
