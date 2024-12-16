
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopHandler : MonoBehaviour
{
    private CardsFactory _cardsFactory;
    private PlaceableUnitsFactory _shopUnitsFactory;
    private WalletHandler _walletHandler;
    private EnvironmentHandler _environmentHandler;
    private SignalBus _signalBus;
    private SellUnitGUIHandler _sellUnitGUIHandler;
    private GUIWarningHandler _guiWarningHandler;

    [SerializeField] private CardsData _cardsData;
    [SerializeField] private Transform _shopSlotsFolder;
    [SerializeField] private Button _buyAdditionalPartsButton;

    [Header("In-Game Params")]
    [SerializeField] private int _additionalPartPrice;
    [SerializeField] private string _constantAdditionalPartsPurchaseText;

    private TextMeshProUGUI _additionalPartsText;
    private List<ShopSlot> _slotsList = new();

    [Inject]
    private void Initialize(
        CardsFactory cardsFactory,
        PlaceableUnitsFactory placeableUnitsFactory,
        WalletHandler walletHandler,
        EnvironmentHandler environmentHandler,
        SignalBus signalBus,
        SellUnitGUIHandler sellUnitGUIHandler,
        GUIWarningHandler guiWarningHandler
        )
    {
        _cardsFactory = cardsFactory;
        _shopUnitsFactory = placeableUnitsFactory;
        _walletHandler = walletHandler;
        _environmentHandler = environmentHandler;
        _signalBus = signalBus;
        _sellUnitGUIHandler = sellUnitGUIHandler;
        _guiWarningHandler = guiWarningHandler;

        _signalBus.Subscribe<WaveEndedSignal>(FillShopSlotsWithCards);
    }

    private void Start()
    {
        for (int i = 0; i < _shopSlotsFolder.childCount; i++)
        {
            if(_shopSlotsFolder.GetChild(i).TryGetComponent(out ShopSlot shopSlot))
                _slotsList.Add(shopSlot);
        }

        _buyAdditionalPartsButton.onClick.AddListener(PurchaseAdditionalParts);
        _additionalPartsText = _buyAdditionalPartsButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _additionalPartsText.text = _additionalPartPrice.ToString() + " " + _constantAdditionalPartsPurchaseText;

        FillShopSlotsWithCards();
    }

    private void Update()
    {
        OpenSellGUIForSelectedUnit();
    }

    public void Purchase(UnitConfig config, GameObject Requester, int price)
    {
        if (_environmentHandler.GetEnvironmentContainer().CanSetItemInBenchSlotOrNot() == false)
        {
            _guiWarningHandler.ShowWarningScreen("Нет свободного места");
            return;
        }
        if (_walletHandler.CurrentMoney < price)
        {
            _guiWarningHandler.ShowWarningScreen("Недостаточно золота");
            return;
        }
        

        _walletHandler.SpendMoney(price);
        if(_environmentHandler.GetEnvironmentContainer().TryToUpgradeUnitsFromShop(config.Name) == false)
            _environmentHandler.GetEnvironmentContainer().SetItemInBenchSlotFromShop(_shopUnitsFactory.ProducePlaceableUnit(config, price));

        Destroy(Requester);
    }

    private void FillShopSlotsWithCards()
    {
        ClearItemsInSlots();

        for (int i = 0; i < _slotsList.Count; i++)
        {
            //try
            //{
            //    var newCard = _cardsFactory.CreateUnitCard(RandomizeCardType());
            //    _slotsList[i].PlaceCardIntoSlot(newCard.transform);
            //}
            //catch (System.Exception)
            //{
            //    Debug.LogError("Creation of unit card goes wrong");
                
            //}

            var newCard = _cardsFactory.CreateUnitCard(RandomizeCardType());
            _slotsList[i].PlaceCardIntoSlot(newCard.transform);

        }
    }

    private void ClearItemsInSlots()
    {
        for (int i = 0; i < _slotsList.Count; i++)
        {
            _slotsList[i].DestroyItem();
        }
    }

    private string RandomizeCardType()
    {
        int randomValue = Random.Range(0, 100);
        
        int tier = 0;
        string cardPrefabName = string.Empty;

        if (randomValue > 100 - _cardsData.T3Weight)
            tier = 3;

        else if (randomValue > (100 - _cardsData.T3Weight) - _cardsData.T2Weight)
            tier = 2;

        else if (randomValue <= _cardsData.T1Weight)
            tier = 1;

        switch (tier)
        {
            case 1:
                cardPrefabName = _cardsData.T1Cards[Random.Range(0, _cardsData.T1Cards.Count)];
                return "T1/" + cardPrefabName;
            case 2:
                cardPrefabName = _cardsData.T2Cards[Random.Range(0, _cardsData.T2Cards.Count)];
                return "T2/" + cardPrefabName;
            case 3:
                cardPrefabName = _cardsData.T3Cards[Random.Range(0, _cardsData.T3Cards.Count)];
                return "T3/" + cardPrefabName;
            default:
                Debug.LogError("Randomize goes wrong"); 
                return cardPrefabName;
        }

    }

    private void PurchaseAdditionalParts()
    {
        var listRef = _environmentHandler.GetEnvironmentContainer().BuyableTowerParts;

        if (listRef.Count > 0)
        {
            for (int i = 0; i < listRef.Count;)
            {
                if(_walletHandler.CurrentMoney >= _additionalPartPrice)
                {
                    _walletHandler.SpendMoney(_additionalPartPrice);
                    Debug.Log(listRef[i].gameObject.activeSelf + " ACTIVE?");
                    listRef[i].gameObject.SetActive(true);
                    Debug.Log(listRef[i].gameObject.activeSelf + " ACTIVE?");
                    listRef.RemoveAt(i);
                    return;
                }
                else
                {
                    Debug.Log("#NOTIFY TABLE# | НЕТ ДЕНЯХ ");
                    _guiWarningHandler.ShowWarningScreen("Недостаточно золота");
                    return;
                }
            }
        }
    }

    private void OpenSellGUIForSelectedUnit()
    {
        //if raycast is true
        //open interface vec2 = gameobject to workpoint gui PRODAT FOR X GOLD
        //and method for the purchase button that gives gold to wallet
        if(Input.GetMouseButtonDown(1))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.RaycastAll(mousePos, Vector2.right);
            foreach (var collider in hit)
            {
                if (collider.transform.TryGetComponent(out PlaceableUnit pUnit))
                {
                    _sellUnitGUIHandler.ShowSellUnitScreen(pUnit);
                    return;
                }
                    
            }
        }
    }

    public void SellUnit(PlaceableUnit pUnit)
    {
        _walletHandler.AddMoney(pUnit.SellPrice);
        pUnit.ParentSlot.InformOfTakingItemFromSlot();
        Destroy(pUnit.gameObject);
        //close sell screen
    }
}

