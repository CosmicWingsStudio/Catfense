using UnityEngine;

public class UnitUltimate : MonoBehaviour
{
    [SerializeField] private UltimateIcon _icon;
    [SerializeField, Min(10)] private float _ultmateCooldown;

    [Header("Choose one ultimate effect")]

    [SerializeField] private bool EmpoweredDamage = false;
    [SerializeField, Range(0.1f,0.9f)] private float _empoweredDamage = 0.2f;
    [SerializeField] private bool Healing = false;
    [SerializeField] private bool DoubleShot = false;

    private HealthHandler _healthHandler;
    private UnitAttack _attack;
    private PlaceableUnit _unit;

    private float _ultmateRandomisedCooldown;
    private float _cooldownTimer = 0f;
    private bool IsEmpowered = false;
    private bool IsPlaced = false;
    private bool OnWave = false;

    private void Start()
    {
        _healthHandler = GetComponent<HealthHandler>();
        _attack = GetComponent<UnitAttack>();
        _unit = GetComponent<PlaceableUnit>();
        _unit.OnPlacedStatusChanged += status => IsPlaced = status;
        _unit.OnWaveStatusChanged += status => OnWave = status;
        _ultmateRandomisedCooldown = RandomizeUltimateTime();
    }

    private void Update()
    {
        if (!IsPlaced || !OnWave || IsEmpowered)
            return;

        if (Healing && _healthHandler.CurrentHealthPoint == _healthHandler.MaxHealth)
            return;

        if(_cooldownTimer < _ultmateRandomisedCooldown) 
            _cooldownTimer += Time.deltaTime;
        else
        {
            _icon.gameObject.SetActive(true);
            _cooldownTimer = 0f;
        }
    }

    private float RandomizeUltimateTime()
    {
        return Random.Range(_ultmateCooldown, _ultmateCooldown + _ultmateCooldown * 0.5f);
    }

    public void DoUlitmateAction()
    {
        IsEmpowered = true;

        if (EmpoweredDamage)
        {
            _attack.EmpowerDamageNextShot(0.2f);
        }
        else if (Healing)
        {
            _healthHandler.Heal(_healthHandler.MaxHealth * 0.1f);
            UnitPerformedUltimate();
        }
        else if (DoubleShot)
        {
            _attack.EmpowerDoubleShotNextShot();
        }
    }

    public void UnitPerformedUltimate()
    {
        IsEmpowered = false;
        _ultmateRandomisedCooldown = RandomizeUltimateTime();
    }


    private void OnValidate()
    {
        if(EmpoweredDamage == true)
        {
            Healing = false;
            DoubleShot = false;
        }
        else if(Healing == true)
        {
            EmpoweredDamage = false;
            DoubleShot = false;
        }
        else if(DoubleShot == true)
        {
            EmpoweredDamage = false;
            Healing = false;
            if (GetComponent<MeleeUnitAttack>())
                DoubleShot = false;
        }
    }
}
