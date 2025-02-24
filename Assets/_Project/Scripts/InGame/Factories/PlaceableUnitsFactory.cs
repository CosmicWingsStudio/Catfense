using UnityEngine;
using Zenject;

public class PlaceableUnitsFactory : MonoBehaviour
{
    [SerializeField] private GameObject UnitSpawnEffect;
    private PrefabsDataProvider _prefabsDataProvider;
    private DiContainer _container;

    [Inject]
    private void Initialize(PrefabsDataProvider prefabsDataProvider, DiContainer container)
    {
        _prefabsDataProvider = prefabsDataProvider;
        _container = container;
    }

    public PlaceableUnit ProducePlaceableUnit(UnitConfig config, int originalPrice)
    {   
        PlaceableUnit pUnit = Instantiate(Resources.Load<PlaceableUnit>(_prefabsDataProvider.GetPlayerUnitsPrefabsPath() + config.PrefabName));
        pUnit.Initialize(originalPrice, config);   
        return pUnit;
    }
}
