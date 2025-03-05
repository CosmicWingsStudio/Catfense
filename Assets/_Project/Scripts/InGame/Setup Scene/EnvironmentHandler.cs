
using UnityEngine;
using Zenject;

public class EnvironmentHandler : MonoBehaviour
{
    PrefabsPathsToFoldersProvider _prefabsData;
    SignalBus _signalBus;
    TeamEffects _teamEffects;

    [SerializeField] private Transform _unitsFolder;

    private EnvironmentContainerHandler _eContainerHandler;

    [Inject]
    private void Initialize(SignalBus signalBus, PrefabsPathsToFoldersProvider prefabsData, TeamEffects teamEffects)
    {
        _prefabsData = prefabsData;
        _signalBus = signalBus;
        _teamEffects = teamEffects;
    }

    public void SetEnvironment(string PrefabName)
    {
        _eContainerHandler = Instantiate(Resources.Load<EnvironmentContainerHandler>(_prefabsData.EnvironmentPrefabsPath + PrefabName), transform);
        _eContainerHandler.SetZenjectData(_signalBus, _teamEffects);
    }

    public Transform GetEnemySpawnPoint() => _eContainerHandler.EnemySpawnPoint;

    public EnvironmentContainerHandler GetEnvironmentContainer() => _eContainerHandler;

}
