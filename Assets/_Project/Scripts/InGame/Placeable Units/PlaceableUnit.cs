
using UnityEngine;
using Zenject;

public class PlaceableUnit : MonoBehaviour
{
    public bool IsBenched
    {
        get
        {
            if(transform.parent != null && transform.parent.gameObject.GetComponent<BenchSlot>())
                return true;
            else
                return false;
        }
        set{}
    }
    public bool IsPlaced
    {
        get
        {
            if (transform.parent != null && transform.parent.gameObject.GetComponent<PlaceSlot>())
                return true;
            else
                return false;
        }
        set {}
    }

    public Slot2D ParentSlot
    {
        get
        {
            return transform.parent.GetComponent<Slot2D>();
        }
    }

    public int SellPrice { get; private set; }

    private bool IsIntialised = false;

    public void Initialize(int originalPrice)
    {
        if(IsIntialised == false)
        {
            SellPrice = originalPrice / 2; 
            IsIntialised = true;
        }
    } 
}
