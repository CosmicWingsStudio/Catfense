
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentContainerHandler : MonoBehaviour
{
    [SerializeField] private Transform BenchSlotsFolder;
    [SerializeField] private Transform BuyableTowerPartsFolder;

    [field: SerializeField] public Transform EnemySpawnPoint { get; private set; }

    [HideInInspector] public List<BenchSlot> BenchSlots = new();
    [HideInInspector] public List<AdditionalTowerPart> BuyableTowerParts = new();

    private void Start()
    {
        if (BenchSlotsFolder != null)
            for (int i = 0; i < BenchSlotsFolder.childCount; i++)
            {
                BenchSlots.Add(BenchSlotsFolder.GetChild(i).GetComponent<BenchSlot>());
            }

        if(BuyableTowerPartsFolder != null)
            for (int i = 0; i < BuyableTowerPartsFolder.childCount; i++)
            {
                BuyableTowerParts.Add(BuyableTowerPartsFolder.GetChild(i).GetComponent<AdditionalTowerPart>());
            }
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

        Debug.Log("#NOTIFY TABLE# | мер ябнандмшу якнрнб мю кюбе ");
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
}
