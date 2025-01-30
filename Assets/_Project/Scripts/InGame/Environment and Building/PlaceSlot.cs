using UnityEngine;

public class PlaceSlot : Slot2D
{
    public override void PlaceItemInSlot(Transform item)
    {
        base.PlaceItemInSlot(item);
        item.GetComponent<PlaceableUnit>().TurnOnActiveMode();
    }

    public override void SwapItemWithAnotherItem(Transform item)
    {
        base.SwapItemWithAnotherItem(item);
        item.GetComponent<PlaceableUnit>().TurnOnActiveMode();
    }
}
