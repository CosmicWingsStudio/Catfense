
using System;
using System.Collections;
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
        set { OnPlacedStatusChanged?.Invoke(value); }
    }

    public bool OnWave
    {
        set { OnWaveStatusChanged?.Invoke(value); }
    }

    public Slot2D ParentSlot
    {
        get
        {
            return transform.parent.GetComponent<Slot2D>();
        }
    }

    public int SellPrice
    {
        get
        {
            switch (GetCurrentUnitLevel())
            {
                case 0:
                    return _defaultSellPrice;
                case 1:
                    return _defaultSellPrice * 3;
                case 2:
                    return _defaultSellPrice * 6;
                case 3:
                    return _defaultSellPrice * 9;
                default:
                    return _defaultSellPrice;
            }
        }
    }

    public UnitAttack unitAttackHandler { get; private set; }

    public UnitDataDisplayer DataDisplayer {get; private set;}

    public SpriteRenderer spriteRenderer { get; private set; }

    public HealthHandler Health { get; private set; }

    public int DefaultSortingOrder { get; private set; }

    public string Name { get; private set; }

    public TeamEffectsType TeamEffectCollection { get; private set; }

    private UnitUpgrader _unitUpgrader;
    private Animator _animator;

    private int _defaultSellPrice;
    private bool IsIntialised = false;

    public event Action<bool> OnPlacedStatusChanged;
    public event Action<bool> OnWaveStatusChanged;
    [HideInInspector] public bool OnSaleScreen = false;  


    public void Initialize(int originalPrice, UnitConfig config)
    {
        if(IsIntialised == false)
        {
            _defaultSellPrice = originalPrice / 2;

            Name = config.Name;
            TeamEffectCollection = config.TeamEffectCollection;

            DataDisplayer = GetComponent<UnitDataDisplayer>();
            _unitUpgrader = GetComponent<UnitUpgrader>();
            Health = GetComponent<HealthHandler>();
            unitAttackHandler = GetComponent<UnitAttack>();

            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).TryGetComponent(out SpriteRenderer sr))
                {
                    spriteRenderer = sr;

                    if (transform.GetChild(i).TryGetComponent(out Animator anim))
                    {
                        _animator = anim;
                        break;
                    }
                }
                
                
            }

            DefaultSortingOrder = spriteRenderer.sortingOrder;

            DataDisplayer.UnitName = Name;
            DataDisplayer.SetDisplayInformation();

            Health.SetHealthParams(config.HealthPoints, DataDisplayer._hpSlider);

            unitAttackHandler.SetData(config);
            unitAttackHandler.Animator = _animator;
            TurnOffActiveMode();

            StartCoroutine(SetCanvasPriorityDelay(transform.GetChild(1).GetComponent<Canvas>()));
            StartCoroutine(SpriteVisibilityDelay());
            IsIntialised = true;
        }
    }

    private IEnumerator SpriteVisibilityDelay()
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.6f);
        spriteRenderer.enabled = true;
    }

    private IEnumerator SetCanvasPriorityDelay(Canvas canvas)
    {
        canvas.enabled = false;
        yield return new WaitForSeconds(0.9f);
        canvas.enabled = true;
        canvas.sortingOrder = 22;
    }

    public void SendRequestForUpgrade()
    {
        _unitUpgrader.UpgradeUnit();
    }

    public void TurnOffActiveMode(bool turnOffBecauseDragging = false)
    {
        unitAttackHandler.TurnOffAttackMode();

        if (!turnOffBecauseDragging)
            _animator.Rebind();

        _animator.enabled = false;
    }

    public void TurnOnActiveMode()
    {
        unitAttackHandler.TurnOnAttackMode();
        _animator.enabled = true;
    }


    public int GetCurrentUnitLevel() => _unitUpgrader.GetCurrentUpgradeLevel();

    public int GetMaxUpgradeLevel() => _unitUpgrader.MaxUpgradeLevel;

}
