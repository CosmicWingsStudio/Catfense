using UnityEngine;

public abstract class Slot2D : MonoBehaviour
{
    [SerializeField] private float _defaultOffsetY;
    protected BoxCollider2D _collider;

    public virtual GameObject Item
    {
        get
        {
            if (transform.childCount > 1)
                return transform.GetChild(1).gameObject;
            else
                return null;
        }
    }

    protected virtual void Start()
    {
        if (GetComponent<BoxCollider2D>() != null)
            _collider = GetComponent<BoxCollider2D>();

        if (Item != null)
            _collider.enabled = false;
        else
            _collider.enabled = true;
    }

    public virtual void PlaceItemInSlot(Transform item)
    {
        item.SetParent(transform);
        //item.localPosition = Vector2.zero;
        Vector2 newpos = new(0, _defaultOffsetY);
        item.localPosition = newpos;
        ChangeColliderState(false);
    }

    public virtual void SwapItemWithAnotherItem(Transform item)
    {
        item.SetParent(transform);
        item.localPosition = Vector2.zero;
    }

    public virtual void InformOfTakingItemFromSlot()
    {
        ChangeColliderState(true);
    }

    protected void ChangeColliderState(bool state) => _collider.enabled = state;
}
