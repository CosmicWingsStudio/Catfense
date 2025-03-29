
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    [SerializeField] private float _detectionSizeX;
    [SerializeField] private float _detectionSizeY;

    protected UnitAttack _attackHandler;
    public bool IsStoped { get; set; } = false;

    private void Start()
    {
        _attackHandler = GetComponent<UnitAttack>();
    }

    private void FixedUpdate()
    {
        if (IsStoped)
            return;

        if(_attackHandler.CurrentTarget == null)
            FindClosestTarget();
    }
    protected virtual void AnalyzeDetectedCollider(Collider2D collider)
    {
        if (collider.TryGetComponent(out EnemyUnit enemy))
        {
            if (_attackHandler.CurrentTarget == null)
            {
                
                _attackHandler.SetCurrentTarget(enemy.transform);

            }
        }
    }

    public void FindClosestTarget()
    {
        var colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(_detectionSizeX, _detectionSizeY), 90); 
        EnemyUnit closestEnemy = null;
        Vector2 closestEnemyVector = Vector2.zero;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out EnemyUnit enemy))
            {
                if (closestEnemy != null)
                {
                    Vector2 newVec = enemy.transform.position - transform.position;
                    if (newVec.sqrMagnitude < closestEnemyVector.sqrMagnitude)
                    {
                        closestEnemy = enemy;
                        closestEnemyVector = newVec;
                    }
                }
                else
                {
                    closestEnemy = enemy;
                    closestEnemyVector = enemy.transform.position - transform.position;
                }
            }
        }
        if (closestEnemy != null)
            _attackHandler.SetCurrentTarget(closestEnemy.transform);
    }

    
}
