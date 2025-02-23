using UnityEngine;

public class EnemyRangeUnitAttack : EnemyAttack
{
    private EnemyMovement _enemyMovement;
    [SerializeField] private string _projectailPrefabPath;
    [SerializeField] private float _projectailSpeed;
    [SerializeField] private float _shootPointOffsetX;
    [SerializeField] private float _shootPointOffsetY;

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

    public override void SetCurrentTarget(Transform target)
    {
        base.SetCurrentTarget(target);
        _enemyMovement.CanMove = false;
        _animator.SetBool("CanMove", false);
    }

    public override void AttackAnimationPoint()
    {
        InAnimation = false;
        if (CurrentTarget != null)
        {
            Projectail projectail = Instantiate(Resources.Load<Projectail>(_projectailPrefabPath), transform);
            Vector2 newPos = new(projectail.transform.position.x + _shootPointOffsetX, projectail.transform.position.y + _shootPointOffsetY);
            projectail.transform.position = newPos;
            Debug.Log("T " + projectail.transform.rotation);
            projectail.Initialize(Damage, _projectailSpeed, CurrentTarget.transform);
            Debug.Log("T " + projectail.transform.rotation);
        }
           
    }
}
