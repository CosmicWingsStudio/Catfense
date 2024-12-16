using UnityEngine;

public class MeleeUnitAttack : UnitAttack
{
     
    public override void SetData(UnitConfig config)
    {
        _firerate = config.Firerate;
        _damage = config.Damage;
        _originalDamage = _damage;
    }

    protected override void AttackAnimationPoint()
    {
        CurrentTarget.GetComponent<HealthHandler>().TakeDamage(_damage);
    }
}
