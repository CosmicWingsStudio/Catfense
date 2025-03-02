

public class MeleeUnitAttack : UnitAttack
{
     
    public override void SetData(UnitConfig config)
    {
        _firerate = config.Firerate;
        _damage = config.Damage;
        _originalDamage = _damage;
    }

    public override void AttackAnimationPoint()
    {
        InAnimation = false;
        _audioSource.clip = _attackSoundClip;
        _audioSource.Play();
        if(CurrentTarget != null)
        {
            HealthHandler enemy = CurrentTarget.GetComponent<HealthHandler>();
            if (OnEmpoweredShot)
            {
                enemy.TakeDamage(_damage + _empoweredDamage);
                OnEmpoweredShot = false;
                _unitUltimate.UnitPerformedUltimate();
            }
            else
                enemy.TakeDamage(_damage + _empoweredDamage);
        }
    }
}
