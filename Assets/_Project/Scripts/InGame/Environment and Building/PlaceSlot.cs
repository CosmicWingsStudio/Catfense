using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlaceSlot : Slot2D
{
    [SerializeField] private GameObject DropZone;
    [SerializeField, Tooltip("Set only for InFront slots")] private GameObject DefendFxEffect;

    private UnityEvent ItemChangedSignal;
    private bool IsInititalised = false;

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

    public void Initialize(UnityEvent eventRef)
    {
        if(IsInititalised == false)
        {
            ItemChangedSignal = eventRef;
            IsInititalised = true;
        }
    }

    public override void PlaceItemInSlot(Transform item)
    {
        base.PlaceItemInSlot(item);
        PlaceableUnit unit = item.GetComponent<PlaceableUnit>();
        unit.TurnOnActiveMode();
        unit.IsPlaced = true;
        ItemChangedSignal.Invoke();
    }

    public override void SwapItemWithAnotherItem(Transform item)
    {
        base.SwapItemWithAnotherItem(item);
        PlaceableUnit unit = item.GetComponent<PlaceableUnit>();
        unit.TurnOnActiveMode();
        unit.IsPlaced = true;
        ItemChangedSignal.Invoke();
    }

    public override void InformOfTakingItemFromSlot()
    {
        base.InformOfTakingItemFromSlot();
        ItemChangedSignal.Invoke();
    }

    public void ShowDropZone()
    {
        DropZone.SetActive(true);
    }

    public void HideDropZone()
    {
        DropZone.SetActive(false);
    }

    public bool SetDefendEffect(float time)
    {
        if(Item != null)
        {
            Item.GetComponent<HealthHandler>().SetInvincibleEffect(time);
            DefendFxEffect.SetActive(true);
            StartCoroutine(DefendEffect(time));
            return true;
        }
        else
            return false;
    }

    public void UpdateSlotAfterWave()
    {
        if(Item == null)
            ChangeColliderState(true);
        else
            ChangeColliderState(false);
    }

    private IEnumerator DefendEffect(float time)
    {
        yield return new WaitForSeconds(time);
        DefendFxEffect.SetActive(false);
    }
}
