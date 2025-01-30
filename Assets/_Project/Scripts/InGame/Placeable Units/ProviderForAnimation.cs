using UnityEngine;

public class ProviderForAnimation : MonoBehaviour
{
    private UnitAttack unitAttackHandler;

    private void Start()
    {
        unitAttackHandler = transform.parent.GetComponent<UnitAttack>();
    }

    public void ShootEvent()
    {
        unitAttackHandler.AttackAnimationPoint();
    }
}
