
using UnityEngine;

[RequireComponent(typeof(HealthHandler))]
[RequireComponent(typeof(UnitDataDisplayer))]
[RequireComponent(typeof(UnitUpgrader))]
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

    public UnitAttack unitAttackHandler { get; private set; }

    public UnitDataDisplayer DataDisplayer {get; private set;}

    public SpriteRenderer spriteRenderer { get; private set; }

    public HealthHandler Health { get; private set; }

    public int DefaultSortingOrder { get; private set; }

    public string Name { get; private set; }


    private UnitUpgrader _unitUpgrader;
    private bool IsIntialised = false;

    public void Initialize(int originalPrice, UnitConfig config)
    {
        if(IsIntialised == false)
        {
            SellPrice = originalPrice / 2; 
            Name = config.Name;

            DataDisplayer = GetComponent<UnitDataDisplayer>();
            _unitUpgrader = GetComponent<UnitUpgrader>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            Health = GetComponent<HealthHandler>();
            unitAttackHandler = GetComponent<UnitAttack>();

            DefaultSortingOrder = spriteRenderer.sortingOrder;

            DataDisplayer.UnitName = Name;
            DataDisplayer.SetDisplayInformation();

            Health.SetHealthParams(config.HealthPoints, DataDisplayer._hpSlider);

            unitAttackHandler.SetData(config);
            TurnOffAttackMode();

            IsIntialised = true;
        }
    } 

    public void SendRequestForUpgrade()
    {
        _unitUpgrader.UpgradeUnit();
    }

    public void TurnOffAttackMode()
    {
        unitAttackHandler.TurnOffAttackMode();
    }

    public void TurnOnAttackMode()
    {
        unitAttackHandler.TurnOnAttackMode();
    }

    public int GetCurrentUnitLevel() => _unitUpgrader.GetCurrentUpgradeLevel();

    public int GetMaxUpgradeLevel() => _unitUpgrader.MaxUpgradeLevel;

}
