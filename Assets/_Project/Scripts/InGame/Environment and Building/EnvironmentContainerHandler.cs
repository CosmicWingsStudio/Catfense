
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnvironmentContainerHandler : MonoBehaviour
{
    [SerializeField] private Transform BenchSlotsFolder;
    [SerializeField] private Transform PlaceSlotsFolder;
    [SerializeField] protected TowerHealthHandler _towerHealthHandler;
    private List<PlaceSlot> AllPlaceSlots = new();
    private SignalBus _signalBus;

    [SerializeField] private Transform BuyableTowerPartsFolder;

    [field: SerializeField] public Transform EnemySpawnPoint { get; private set; }

    [HideInInspector] public List<BenchSlot> BenchSlots = new();
    [HideInInspector] public List<PlaceSlot> PlaceSlots = new();
    [HideInInspector] public List<Slot2D> AllSlots = new();
    [HideInInspector] public List<AdditionalTowerPart> BuyableTowerParts = new();

    private void Start()
    {
        if (BenchSlotsFolder != null)
            for (int i = 0; i < BenchSlotsFolder.childCount; i++)
            {
                BenchSlots.Add(BenchSlotsFolder.GetChild(i).GetComponent<BenchSlot>());
            }

        if (PlaceSlotsFolder != null)
            for (int i = 0; i < PlaceSlotsFolder.childCount; i++)
            {
                PlaceSlots.Add(PlaceSlotsFolder.GetChild(i).GetComponent<PlaceSlot>());
            }

        if (BuyableTowerPartsFolder != null)
            for (int i = 0; i < BuyableTowerPartsFolder.childCount; i++)
            {
                AdditionalTowerPart twp = BuyableTowerPartsFolder.GetChild(i).GetComponent<AdditionalTowerPart>();
                BuyableTowerParts.Add(twp);
                
                for (int j = 0; j < twp.BuyableSlotsFolder.childCount; j++)
                {
                    PlaceSlots.Add(twp.BuyableSlotsFolder.GetChild(j).GetComponent<PlaceSlot>());
                }
            }

        for (int i = 0; i < PlaceSlots.Count; i++)
        {
            AllSlots.Add(PlaceSlots[i]);
        }

        for (int i = 0; i < BenchSlots.Count; i++)
        {
            AllSlots.Add(BenchSlots[i]);
        }

        for (int i = 0; i < AllSlots.Count; i++)
        {
            if (AllSlots[i].GetComponent<PlaceSlot>())
                AllPlaceSlots.Add((PlaceSlot)AllSlots[i]);
        }
    }

    public void SetZenjectData(SignalBus signalBus)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<WaveEndedSignal>(AfterWaveActions);

        _towerHealthHandler.SetSignalBus(signalBus);
    }

    public bool TrySetItemInBenchSlot(Transform item)
    {
        foreach (var slot in BenchSlots)
        {
            if(slot.Item == null)
            {
                slot.PlaceItemInSlot(item);
                return true;
            }
           
        }

        return false;
    }

    public void SetItemInBenchSlotFromShop(PlaceableUnit pUnit)
    {
        foreach (var slot in BenchSlots)
        {
            if (slot.Item == null)
            {
                slot.PlaceItemInSlot(pUnit.transform);
                FxSpawner.Instance.SpawnPurchaseEffect(pUnit.transform.position);
                return;
            }

        }
    }

    public bool TryToUpgradeUnitsFromShop(string IncomingNewUnitName)
    {
        int UnitsNumber = 1;
        int firstItem = 0;

        for (int i = 0; i < AllSlots.Count; i++)
        {
            if (AllSlots[i].Item != null && AllSlots[i].Item.TryGetComponent(out PlaceableUnit punit)
                && punit.Name == IncomingNewUnitName && punit.GetCurrentUnitLevel() == 0) 
            {               

                if (UnitsNumber + 1 == 3)
                {
                    punit.ParentSlot.InformOfTakingItemFromSlot();
                    Destroy(AllSlots[i].Item);
                    PlaceableUnit curItem = AllSlots[firstItem].Item.GetComponent<PlaceableUnit>();
                    curItem.SendRequestForUpgrade();
                    TryToUpgrade(curItem.Name, curItem.GetCurrentUnitLevel());
                    return true;
                }
                else
                {
                    UnitsNumber++;
                    firstItem = i;
                }
            }
        }

        return false;
    }

    public bool TryToUpgrade(string checkUnitName, int currentLevel)
    {
        int UnitsNumber = 1;
        int firstItem = 0;

        for (int i = 0; i < AllSlots.Count; i++)
        {
            if (AllSlots[i].Item != null && AllSlots[i].Item.TryGetComponent(out PlaceableUnit punit)
                && punit.Name == checkUnitName && punit.GetCurrentUnitLevel() == currentLevel)
            {

                if (UnitsNumber + 1 == 3)
                {
                    punit.ParentSlot.InformOfTakingItemFromSlot();
                    Destroy(AllSlots[i].Item);

                    PlaceableUnit curItem = AllSlots[firstItem].Item.GetComponent<PlaceableUnit>();
                    curItem.SendRequestForUpgrade();
                    if(curItem.GetCurrentUnitLevel() < curItem.GetMaxUpgradeLevel())
                        TryToUpgrade(curItem.Name, curItem.GetCurrentUnitLevel());
                    return true;
                }
                else
                {
                    UnitsNumber++;
                    firstItem = i;
                }
            }
        }

        return false;
    }

    public bool CanSetItemInBenchSlotOrNot()
    {
        foreach (var slot in BenchSlots)
        {
            if (slot.Item == null)
            { 
                return true;
            }
        }
 
        return false;
    }

    public List<PlaceSlot> GetAllPlaceableSlots()
    {   
        return AllPlaceSlots;
    }

    private void AfterWaveActions()
    {
        HealUnits();
    }

    private void HealUnits()
    {
        List<PlaceableUnit> unitsList = new();
        for (int i = 0; i < AllSlots.Count; i++)
        {
            if (AllSlots[i].Item != null)
            {
                unitsList.Add(AllSlots[i].Item.GetComponent<PlaceableUnit>());
            }
        }

        for (int i = 0; i < unitsList.Count; i++)
        {
            unitsList[i].Health.Heal(unitsList[i].Health.MaxHealth * 0.5f);
        }
    }
}
