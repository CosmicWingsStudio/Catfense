
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class EnvironmentContainerHandler : MonoBehaviour
{
    [SerializeField] private Transform BenchSlotsFolder;
    [SerializeField] private Transform PlaceSlotsFolder;
    [SerializeField] private PlaceSlot InFrontSlot;
    [SerializeField] protected TowerHealthHandler _towerHealthHandler;
    private List<PlaceSlot> AllPlaceSlots = new();
    private SignalBus _signalBus;
    private TeamEffects _teamEffects;
    private UnityEvent OnChangedUnitInPlaceSlot = new UnityEvent();
    private bool IsOnUpdateTeamEffects = false;
    public float HealingBonus { get; set; }

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

        OnChangedUnitInPlaceSlot.AddListener(UpdateTeamEffects);

        for (int i = 0; i < PlaceSlots.Count; i++)
        {
            PlaceSlots[i].Initialize(OnChangedUnitInPlaceSlot);
        }
    }

    public void SetZenjectData(SignalBus signalBus, TeamEffects te)
    {
        _signalBus = signalBus;
        _teamEffects = te;
        _signalBus.Subscribe<WaveEndedSignal>(AfterWaveActions);
        _signalBus.Subscribe<WaveStartedSignal>(AfterWaveStartedActions);

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

    public PlaceSlot GetInFrontSlot()
    {
        return InFrontSlot;
    }

    private void AfterWaveActions()
    {
        HealUnits();
        ChangeStatusToPlacedUnits(false);
    }

    private void AfterWaveStartedActions()
    {
        HealUnits();
        ChangeStatusToPlacedUnits(true);
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

            unitsList[i].Health.Heal(unitsList[i].Health.MaxHealth * (0.5f + HealingBonus));
        }
    }

    private void ChangeStatusToPlacedUnits(bool OnWaveStatus)
    {
        for (int i = 0; i < PlaceSlots.Count; i++)
        {
            GameObject item = PlaceSlots[i].Item;
            if(item != null && item.TryGetComponent(out PlaceableUnit punit))
            {
                punit.OnWave = OnWaveStatus;
            }
        }
    }

    private void UpdateTeamEffects()
    {
        if(IsOnUpdateTeamEffects == false)
        {
            IsOnUpdateTeamEffects = true;
            StartCoroutine(UpdateTeamEffectsDelay());
        }
    }

    private IEnumerator UpdateTeamEffectsDelay()
    {
        yield return new WaitForNextFrameUnit();
        IsOnUpdateTeamEffects = false;
        List<TeamEffectsType> teamEffects = new();
        List<TeamEffectsData> teamEffectsData = new();

        for (int i = 0; i < PlaceSlots.Count; i++)
        {
            if (PlaceSlots[i].Item != null)
            {
                teamEffects.Add(PlaceSlots[i].Item.GetComponent<PlaceableUnit>().TeamEffectCollection);
            }
        }

        for (int i = 0; i < teamEffects.Count; i++)
        {
            bool foundEffect = false;

            if(teamEffectsData.Count < 1)
            {
                teamEffectsData.Add(new(teamEffects[i]));
                teamEffectsData[i].AddValue(1);
                
            }
            else
            {
                for (int j = 0; j < teamEffectsData.Count; j++)
                {
                    if (teamEffects[i] == teamEffectsData[j].EffectType)
                    {
                        teamEffectsData[j].AddValue(1);
                        foundEffect = true;
                    }
                }
                if(foundEffect == false)
                {
                    teamEffectsData.Add(new(teamEffects[i]));
                    teamEffectsData[teamEffectsData.Count - 1].AddValue(1);
                }
            }  
        }

        _teamEffects.UpdateEffects(teamEffectsData);
    }
}
