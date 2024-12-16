using UnityEngine;

public class EnemyTargetDetector : TargetDetector
{

    protected override void AnalyzeDetectedCollider(Collider2D collider)
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
