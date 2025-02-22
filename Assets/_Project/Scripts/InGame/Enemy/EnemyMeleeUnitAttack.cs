using UnityEngine;

public class EnemyMeleeUnitAttack : EnemyAttack
{
    private EnemyMovement _enemyMovement;
    private void Start()
    {
        _enemyMovement = GetComponent<EnemyMovement>();    
    }
    protected override void NullifyCurrentTarget()
    {
        base.NullifyCurrentTarget();
        _enemyMovement.CanMove = true;
        _animator.SetBool("CanMove", true);
    }

    public override void AttackAnimationPoint()
    {
        InAnimation = false;
        if(CurrentTarget != null)
            CurrentTarget.GetComponent<HealthHandler>().TakeDamage(Damage);
    }

    public override void SetCurrentTarget(Transform target)
    {
        base.SetCurrentTarget(target);
        _enemyMovement.CanMove = false;
        _animator.SetBool("CanMove", false);
    }
}
