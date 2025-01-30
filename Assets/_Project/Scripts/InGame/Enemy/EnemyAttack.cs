using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private TargetDetector _targetDetector;

    public Transform CurrentTarget { get; set; }
    //public Animator Animator { protected get; set; }

    protected bool IsAttacking = false;
    protected bool InAnimation = false;
    [SerializeField] protected float _firerate;
    protected float _originalFirerate;
    protected float _fireRateCounter = 0f;
    [SerializeField] protected float _damage;
    protected float _originalDamage;

    private void Awake()
    {
        _targetDetector = GetComponent<TargetDetector>();
    } 

    private void Update()
    {
        if (!IsAttacking && !InAnimation)
            return;

        if (_fireRateCounter >= _firerate)
        {
            if (CurrentTarget == null)
            {
                NullifyCurrentTarget();
                return;
            }

            _fireRateCounter = 0f;
            Attack();
        }
        else
            _fireRateCounter += Time.deltaTime;
    }

    protected void Attack()
    {
        //анимации ну типа перенести в јтакјниматионѕоинт спавн снар€да наверн потом
        //звуки
        //InAnimation = true;
        //Animator.SetTrigger("Shoot");
        AttackAnimationPoint();
    }

    public virtual void AttackAnimationPoint()
    {
        //InAnimation = false;
    }

    public virtual void SetCurrentTarget(Transform target)
    {
        CurrentTarget = target;
        _fireRateCounter = _firerate;
        IsAttacking = true;
    }

    protected virtual void NullifyCurrentTarget()
    {
        CurrentTarget = null;
        IsAttacking = false;
    }

    public void UpgradeStats(int multiplier)
    {
        _damage += multiplier * (_originalDamage / 10);
        _firerate -= multiplier * (_originalFirerate / 10);
    }

    public void TurnOffAttackMode()
    {
        _targetDetector.IsStoped = true;
        IsAttacking = false;
        CurrentTarget = null;
    }

    public void TurnOnAttackMode()
    {
        _targetDetector.IsStoped = false;
    }
}
