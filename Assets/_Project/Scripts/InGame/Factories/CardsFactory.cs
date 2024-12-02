
using UnityEngine;
using Zenject;

public class CardsFactory : MonoBehaviour
{
    private PrefabsDataProvider _prefabsDataProvider;
    private DiContainer _container;

    [Inject]
    private void Initialize(PrefabsDataProvider prefabsDataProvider, DiContainer container)
    {
        _prefabsDataProvider = prefabsDataProvider;
        _container = container;
    }

    public UnitCard CreateUnitCard(string prefabName)
    {
        return _container.InstantiatePrefabResource(_prefabsDataProvider.GetCardsPrefabsPath() + prefabName).GetComponent<UnitCard>();
    }
}
