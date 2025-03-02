using UnityEngine;

public class BenchSlot : Slot2D
{

    public override void PlaceItemInSlot(Transform item)
    {
        base.PlaceItemInSlot(item);
        PlaceableUnit unit = item.GetComponent<PlaceableUnit>();
        unit.GetComponent<PlaceableUnit>().TurnOffActiveMode();
        unit.IsPlaced = false;
    }

    public override void SwapItemWithAnotherItem(Transform item)
    {
        base.SwapItemWithAnotherItem(item);
        PlaceableUnit unit = item.GetComponent<PlaceableUnit>();
        item.GetComponent<PlaceableUnit>().TurnOffActiveMode();
        unit.IsPlaced = false;    
    }
}
