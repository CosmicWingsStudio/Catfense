
using UnityEngine;
using Zenject;

public class CardsFactory : MonoBehaviour
{
    [SerializeField] private Transform _baseSlotForInstantiate;
    private PrefabsDataProvider _prefabsDataProvider;
    private DiContainer _container;

    [Inject]
    private void Initialize(PrefabsDataProvider prefabsDataProvider, DiContainer container)
    {
        _prefabsDataProvider = prefabsDataProvider;
        _container = container;
    }

    public UnitCard CreateUnitCard(int cardtier)
    {
        switch (cardtier)
        {
            case 1:
                return _container.InstantiatePrefabResource(_prefabsDataProvider.GetCardsPrefabsPath() + "T1/Card",
                    _baseSlotForInstantiate).GetComponent<UnitCard>();
            case 2:
                return _container.InstantiatePrefabResource(_prefabsDataProvider.GetCardsPrefabsPath() + "T2/Card",
                    _baseSlotForInstantiate).GetComponent<UnitCard>();
            case 3:
                return _container.InstantiatePrefabResource(_prefabsDataProvider.GetCardsPrefabsPath() + "T3/Card",
                    _baseSlotForInstantiate).GetComponent<UnitCard>();
            default:
                Debug.LogError("Wrong card tier been given");
                return null;
        }
    }
}
