using UnityEngine;

public class PlaceSlot : Slot2D
{
    [SerializeField] private GameObject DropZone;
    public override GameObject Item
    {
        get
        {
            if (transform.childCount > 2)
                return transform.GetChild(2).gameObject;
            else
                return null;
        }
    }
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

    public void ShowDropZone()
    {
        DropZone.SetActive(true);
    }

    public void HideDropZone()
    {
        DropZone.SetActive(false);
    }
}
