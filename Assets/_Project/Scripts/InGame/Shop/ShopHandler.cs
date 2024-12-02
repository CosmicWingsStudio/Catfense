using UnityEngine;
using Zenject;

public class ShopHandler : MonoBehaviour
{
    private CardsFactory _cardsFactory;
    private PlaceableUnitsFactory _shopUnitsFactory;
    private WalletHandler _walletHandler;
    private EnvironmentHandler _environmentHandler;

    [Inject]
    private void Initialize(CardsFactory cardsFactory, PlaceableUnitsFactory placeableUnitsFactory,
        WalletHandler walletHandler, EnvironmentHandler environmentHandler)
    {
        _cardsFactory = cardsFactory;
        _shopUnitsFactory = placeableUnitsFactory;
        _walletHandler = walletHandler;
        _environmentHandler = environmentHandler;
    }

    public void Purchase(int price, string prefabName, GameObject Requester)
    {
        if (_environmentHandler.GetEnvironmentContainer().CanSetItemInBenchSlotOrNot() == false)
        {
            Debug.Log("#NOTIFY TABLE# | НЕТ СВОБОДНЫХ СЛОТОВ НА ЛАВКЕ ");
            return;
        }
        if (_walletHandler.CurrentMoney < price)
        {
            Debug.Log("#NOTIFY TABLE# | НЕТ ДЕНЯХ ");
            return;
        }
        

        _walletHandler.SpendMoney(price);
        _environmentHandler.GetEnvironmentContainer().TrySetItemInBenchSlot(_shopUnitsFactory.ProducePlaceableUnit(prefabName).transform);
        Destroy(Requester);
        //снимаем бабки
        //удаляем карточку
        //добавляем юнита
    }
}
