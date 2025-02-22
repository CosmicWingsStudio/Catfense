using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private TargetDetector _targetDetector;

    public Transform CurrentTarget { get; set; }
    //public Animator Animator { protected get; set; }
    protected Animator _animator;

    protected bool IsAttacking = false;
    protected bool InAnimation = false;
    [SerializeField] protected float _firerate;
    protected float _originalFirerate;
    protected float _fireRateCounter = 0f;
    public float Damage;

    private void Awake()
    {
        _targetDetector = GetComponent<TargetDetector>();
        _animator = GetComponent<Animator>();
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
        _animator.SetTrigger("Attack");
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
  
}
