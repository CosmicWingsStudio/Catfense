using UnityEngine;

public class EnemyMeleeUnitAttack : MeleeUnitAttack
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
    }

    public override void SetCurrentTarget(Transform target)
    {
        base.SetCurrentTarget(target);
        _enemyMovement.CanMove = false;
    }
}
