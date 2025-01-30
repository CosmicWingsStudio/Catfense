using UnityEngine;

public class BenchSlot : Slot2D
{

    public override void PlaceItemInSlot(Transform item)
    {
        base.PlaceItemInSlot(item);
        item.GetComponent<PlaceableUnit>().TurnOffActiveMode();
    }

    public override void SwapItemWithAnotherItem(Transform item)
    {
        base.SwapItemWithAnotherItem(item);
        item.GetComponent<PlaceableUnit>().TurnOffActiveMode();
    }
}
