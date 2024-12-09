
using TMPro;
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

    public UnitDataDisplayer DataDisplayer {get; private set;}

    public SpriteRenderer spriteRenderer { get; private set; }

    public int DefaultSortingOrder { get; private set; }

    public string Name { get; private set; }


    private UnitUpgrader _unitUpgrader;

    private bool IsIntialised = false;

    public void Initialize(int originalPrice, string unitName)
    {
        if(IsIntialised == false)
        {
            SellPrice = originalPrice / 2; 
            Name = unitName;

            DataDisplayer = GetComponent<UnitDataDisplayer>();
            _unitUpgrader = GetComponent<UnitUpgrader>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            DefaultSortingOrder = spriteRenderer.sortingOrder;

            DataDisplayer.UnitName = Name;
            DataDisplayer.SetDisplayInformation();

            IsIntialised = true;
        }
    } 

    public void SendRequestForUpgrade()
    {
        _unitUpgrader.UpgradeUnit();
    }

    public int GetCurrentUnitLevel() => _unitUpgrader.GetCurrentUpgradeLevel();
    public int GetMaxUpgradeLevel() => _unitUpgrader.MaxUpgradeLevel;

}
