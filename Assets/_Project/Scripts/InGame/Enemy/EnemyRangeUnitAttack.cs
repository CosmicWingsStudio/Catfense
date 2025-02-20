using UnityEngine;

public class EnemyRangeUnitAttack : EnemyAttack
{
    private EnemyMovement _enemyMovement;
    [SerializeField] private string _projectailPrefabPath;
    [SerializeField] private float _projectailSpeed;

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

    public override void AttackAnimationPoint()
    {
        InAnimation = false;
        Projectail projectail = Instantiate(Resources.Load<Projectail>(_projectailPrefabPath), transform);
        projectail.Initialize(Damage, _projectailSpeed, CurrentTarget.transform);
    }
}
