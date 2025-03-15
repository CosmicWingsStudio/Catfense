using UnityEngine;

public abstract class UnitAttack : MonoBehaviour
{

    [SerializeField] protected AudioClip _attackSoundClip;
    public Transform CurrentTarget { get; set; }
    public Animator Animator { protected get; set; }

    private TargetDetector _targetDetector;
    protected UnitUltimate _unitUltimate;
    protected AudioSource _audioSource;

    protected bool OnEmpoweredShot = false;
    protected bool OnDoubleShot = false;
    protected bool IsAttacking = false;
    protected bool InAnimation = false;
    protected float _empoweredDamage = 0f;
    protected float _firerate;
    protected float _originalFirerate;
    protected float _fireRateCounter = 0f;
    protected float _damage;
    protected float _originalDamage;
    
    private void Awake()
    {
        _targetDetector = GetComponent<TargetDetector>();
        _audioSource = GetComponent<AudioSource>();
        _unitUltimate = GetComponent<UnitUltimate>();
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
        InAnimation = true;
        Animator.SetTrigger("Shoot");
    }

    public virtual void AttackAnimationPoint()
    {
        //InAnimation = false;
        //_attackSoundClip
    }

    public virtual void SetCurrentTarget(Transform target)
    {
        Animator.SetBool("IdleActive", false);
        CurrentTarget = target;
        _fireRateCounter = _firerate;
        IsAttacking = true;
    }

    protected void NullifyCurrentTarget()
    {
        CurrentTarget = null;
        IsAttacking = false;
        Animator.SetBool("IdleActive", true);
    }

    public void UpgradeStats(float multiplier)
    {
        _damage += _originalDamage * multiplier;
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

    public void EmpowerDamageNextShot(float coeffValue)
    {
        OnEmpoweredShot = true;
        _empoweredDamage = _damage * coeffValue;
    }

    public void EmpowerDoubleShotNextShot()
    {
        OnDoubleShot = true;
    }
}
