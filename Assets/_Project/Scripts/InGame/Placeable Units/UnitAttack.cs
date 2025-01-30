using UnityEngine;

public abstract class UnitAttack : MonoBehaviour
{
    private TargetDetector _targetDetector;

    public Transform CurrentTarget { get; set; }
    public Animator Animator { protected get; set; }

    protected bool IsAttacking = false;
    protected bool InAnimation = false;
    protected float _firerate;
    protected float _originalFirerate;
    protected float _fireRateCounter = 0f;
    protected float _damage;
    protected float _originalDamage;

    private void Awake()
    {
        _targetDetector = GetComponent<TargetDetector>();
    }

    public virtual void SetData(UnitConfig config)
    {
        
    }

    public virtual void SetData(float firerate, int damage)
    {
        _firerate = firerate;
        _damage = damage;
        _originalFirerate = _firerate;
        _originalDamage = damage;
    }

    public virtual void SetData(float firerate, int damage, float projectailSpeed, string projectailPrefabPath)
    {

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
        InAnimation = true;
        Animator.SetTrigger("Shoot");
    }

    public virtual void AttackAnimationPoint()
    {
        //InAnimation = false;
    }

    public virtual void SetCurrentTarget(Transform target)
    {
        Animator.SetBool("IdleActive", false);
        CurrentTarget = target;
        _fireRateCounter = _firerate;
        IsAttacking = true;
    }

    protected virtual void NullifyCurrentTarget()
    {
        CurrentTarget = null;
        IsAttacking = false;
        Animator.SetBool("IdleActive", true);
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
