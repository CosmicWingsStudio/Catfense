using UnityEngine;
using Zenject;

public class PlaceableUnitsFactory : MonoBehaviour
{
    private PrefabsDataProvider _prefabsDataProvider;
    private DiContainer _container;

    [Inject]
    private void Initialize(PrefabsDataProvider prefabsDataProvider, DiContainer container)
    {
        _prefabsDataProvider = prefabsDataProvider;
        _container = container;
    }

    public PlaceableUnit ProducePlaceableUnit(string prefabName, int originalPrice, string unitName)
    {
        //return _container.InstantiatePrefabResource(_prefabsDataProvider.GetPlayerUnitsPrefabsPath() + prefabName).GetComponent<PlaceableUnit>();
        PlaceableUnit pUnit = Instantiate(Resources.Load<PlaceableUnit>(_prefabsDataProvider.GetPlayerUnitsPrefabsPath() + prefabName));
        pUnit.Initialize(originalPrice, unitName);
        return pUnit;
    }
}
