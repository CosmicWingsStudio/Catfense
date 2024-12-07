using UnityEngine;

public class ShopSlot : MonoBehaviour
{
    public GameObject Item
    {
        get
        {
            if (transform.childCount > 0)
                return transform.GetChild(0).gameObject;
            else
                return null;
        }
    }

    public void PlaceCardIntoSlot(Transform cardTransform)
    {
        cardTransform.SetParent(transform);
        cardTransform.localPosition = Vector3.zero;
    }
    public void DestroyItem()
    {
        if(Item != null)
            Destroy(Item.gameObject);
    }
}
