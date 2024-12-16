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

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    AnalyzeDetectedCollider(collision);
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    AnalyzeDetectedCollider(collision);
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if(_attackHandler.CurrentTarget != null && collision.gameObject == _attackHandler.CurrentTarget.gameObject)
    //    {
    //        Debug.Log("target exit");
    //        Debug.Log(collision + " exit");
    //        _attackHandler.CurrentTarget = null;
    //    }
    //}

    //protected virtual void AnalyzeDetectedCollider(Collider2D collider)
    //{
    //    if(collider.TryGetComponent(out EnemyUnit enemy))
    //    {
    //        if(_attackHandler.CurrentTarget == null)
    //        {
    //            Debug.Log(collider + " new target");
    //            _attackHandler.SetCurrentTarget(enemy.transform);

    //        }
    //    }
    //}


    private void FixedUpdate()
    {
        if (IsStoped)
            return;

        var colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(_detectionSizeX, _detectionSizeY), 90);
        for (int i = 0; i < colliders.Length; i++)
        {
            AnalyzeDetectedCollider(colliders[i]);
        }
    }
    protected virtual void AnalyzeDetectedCollider(Collider2D collider)
    {
        if (collider.TryGetComponent(out EnemyUnit enemy))
        {
            if (_attackHandler.CurrentTarget == null)
            {
                Debug.Log(collider + " new target");
                _attackHandler.SetCurrentTarget(enemy.transform);

            }
        }
    }
}
