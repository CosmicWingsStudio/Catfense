using UnityEngine;

[RequireComponent(typeof(HealthHandler))]
[RequireComponent(typeof(EnemyDataDisplayer))]
public class EnemyUnit : MonoBehaviour
{
    [Header("Unit parameters")]
    [SerializeField] private int _healthPoints;  

    public HealthHandler unitHealth { get; private set; }
    public EnemyAttack unitAttack { get; private set; }
    public EnemyDataDisplayer DataDisplayer { get; private set; }

    public bool IsInitialised = false;

    private EnemyMovement _enemyMovement;
    private GameObject _moveBlocker;
    private bool isRangeUnit;

    //private void Start()
    //{
    //    unitHealth = GetComponent<HealthHandler>();
    //    unitAttack = GetComponent<EnemyAttack>();
    //    DataDisplayer = GetComponent<EnemyDataDisplayer>();
    //    _enemyMovement = GetComponent<EnemyMovement>();

    //    if (GetComponent<EnemyRangeUnitAttack>())
    //        isRangeUnit = true;

    //    unitHealth.SetHealthParams(_healthPoints, DataDisplayer._hpSlider);

    //}

    public void Initialize(float difficulty)
    {
        if (!IsInitialised)
        {
            unitHealth = GetComponent<HealthHandler>();
            unitAttack = GetComponent<EnemyAttack>();
            DataDisplayer = GetComponent<EnemyDataDisplayer>();
            _enemyMovement = GetComponent<EnemyMovement>();

            if (GetComponent<EnemyRangeUnitAttack>())
                isRangeUnit = true;

            ApplyLevelDifficulty(difficulty);   
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out EnemyUnit eu))
        {
            if(eu.transform.position.x < transform.position.x)
            {
                if (eu.isRangeUnit == true && isRangeUnit == false)
                    return;

                _moveBlocker = eu.gameObject;
                _enemyMovement.CanMove = false;
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_moveBlocker != null && collision.gameObject != null)
        {
            if(collision.gameObject == _moveBlocker)
            {
                if (unitAttack.CurrentTarget == null)
                    _enemyMovement.CanMove = true;
            }   
        }
        else if(_moveBlocker == null)
        {
            if (unitAttack.CurrentTarget == null)
                _enemyMovement.CanMove = true;
        }
    }

    private void ApplyLevelDifficulty(float difficulty)
    {
        float newHp = _healthPoints * difficulty;
        _healthPoints = (int)newHp;

        unitAttack.Damage *= difficulty;
        unitHealth.SetHealthParams(_healthPoints, DataDisplayer._hpSlider);
    }
}
