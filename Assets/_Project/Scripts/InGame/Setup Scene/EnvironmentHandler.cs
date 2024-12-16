
using UnityEngine;
using Zenject;

public class EnvironmentHandler : MonoBehaviour
{
    [Inject] PrefabsPathsToFoldersProvider _prefabsData;
    [Inject] SignalBus _signalBus;

    [SerializeField] private Transform _unitsFolder;

    private EnvironmentContainerHandler _eContainerHandler;

    public void SetEnvironment(string PrefabName)
    {
        _eContainerHandler = Instantiate(Resources.Load<EnvironmentContainerHandler>(_prefabsData.EnvironmentPrefabsPath + PrefabName), transform);
        _eContainerHandler.SetZenjectData(_signalBus);
    }

    public Transform GetEnemySpawnPoint() => _eContainerHandler.EnemySpawnPoint;

    public EnvironmentContainerHandler GetEnvironmentContainer() => _eContainerHandler;

}
