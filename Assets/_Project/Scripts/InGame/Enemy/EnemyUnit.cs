using UnityEngine;

[RequireComponent(typeof(HealthHandler))]
[RequireComponent(typeof(EnemyDataDisplayer))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyTargetDetector))]
public class EnemyUnit : MonoBehaviour
{
    [Header("Unit parameters")]
    [SerializeField] private float _healthPoints;  

    public HealthHandler unitHealth { get; private set; }
    public EnemyAttack unitAttack { get; private set; }
    public EnemyDataDisplayer DataDisplayer { get; private set; }

    public RewardSpawner RewardSpawner { get; private set; }

    [HideInInspector] public bool IsInitialised = false;

    private Animator _animator;
    private EnemyMovement _enemyMovement;
    private GameObject _moveBlocker;
    private bool isRangeUnit;
 
    public void Initialize(float difficulty, RewardSpawner rs, float movementModifire)
    {
        if (!IsInitialised)
        {
            unitHealth = GetComponent<HealthHandler>();
            unitAttack = GetComponent<EnemyAttack>();
            DataDisplayer = GetComponent<EnemyDataDisplayer>();
            _enemyMovement = GetComponent<EnemyMovement>();
            _animator = GetComponent<Animator>();
            RewardSpawner = rs;

            if (GetComponent<EnemyRangeUnitAttack>())
                isRangeUnit = true;

            _enemyMovement.ApplySpeedModifire(movementModifire);
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
                _animator.SetBool("CanMove", false);
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
                {
                    _enemyMovement.CanMove = true;
                    _animator.SetBool("CanMove", true);
                }
            }   
        }
        else if(_moveBlocker == null)
        {
            if (unitAttack.CurrentTarget == null)
            {
                _enemyMovement.CanMove = true;
                _animator.SetBool("CanMove", true);
            }
        }
    }

    private void ApplyLevelDifficulty(float difficulty)
    {
        float newHp = _healthPoints * difficulty;
        Debug.Log(_healthPoints + " "+ difficulty);
        Debug.Log("newhp= " + newHp);
        _healthPoints += newHp;

        unitAttack.Damage += unitAttack.Damage * difficulty;
        unitHealth.SetHealthParams(_healthPoints, DataDisplayer._hpSlider);
    }

}
