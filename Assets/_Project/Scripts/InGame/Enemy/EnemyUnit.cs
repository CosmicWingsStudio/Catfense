using UnityEngine;

[RequireComponent(typeof(HealthHandler))]
[RequireComponent(typeof(EnemyDataDisplayer))]
public class EnemyUnit : MonoBehaviour
{
    [Header("Unit parameters")]
    [SerializeField] private int _healthPoints;
    [SerializeField] private int _damage;
    [SerializeField] private float _firerate;

    [Header("if range unit")]
    [SerializeField] private bool IsRange;
    [SerializeField] private string _projectailPrefabPath;
    [SerializeField] private float _projectailSpeed;

    public HealthHandler unitHealth { get; private set; }
    public UnitAttack unitAttack { get; private set; }
    public EnemyDataDisplayer DataDisplayer { get; private set; }

    private void Start()
    {
        unitHealth = GetComponent<HealthHandler>();
        unitAttack = GetComponent<UnitAttack>();
        DataDisplayer = GetComponent<EnemyDataDisplayer>();

        unitHealth.SetHealthParams(_healthPoints, DataDisplayer._hpSlider);

        if (IsRange)
            unitAttack.SetData(_firerate, _damage, _projectailSpeed, _projectailPrefabPath);
        else
            unitAttack.SetData(_firerate, _damage);
    }

}
