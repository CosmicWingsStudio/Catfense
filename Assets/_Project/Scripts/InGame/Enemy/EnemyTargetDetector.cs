
using UnityEngine;

public class EnemyTargetDetector : MonoBehaviour
{

    [SerializeField, Tooltip("Height")] private float _detectionSizeX;
    [SerializeField, Tooltip("Width")] private float _detectionSizeY;

    private EnemyAttack _attackHandler;

    public bool IsStoped { get; set; } = false;

    private void Start()
    {
        _attackHandler = GetComponent<EnemyAttack>();
    }
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
    protected void AnalyzeDetectedCollider(Collider2D collider)
    {
        if (collider.TryGetComponent(out Tower tower))
        {
            if (_attackHandler.CurrentTarget == null)
            {
                _attackHandler.SetCurrentTarget(tower.transform);
            }
        }
        else if(collider.TryGetComponent(out PlaceableUnit punit))
        {
            if (_attackHandler.CurrentTarget == null && punit.ParentSlot.GetComponent<PlaceSlot>())
            {
                _attackHandler.SetCurrentTarget(punit.transform);
            }
        }   
    }


}
