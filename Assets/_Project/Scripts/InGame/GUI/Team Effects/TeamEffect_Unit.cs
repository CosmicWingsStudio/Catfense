
using UnityEngine;

public abstract class TeamEffect_Unit : UnityEngine.MonoBehaviour
{
    [SerializeField, Range(0.01f, 1f)] protected float _step1;
    [SerializeField, Range(0.01f, 1f)] protected float _step2;
    [SerializeField, Range(0.01f, 1f)] protected float _step3;

    protected int _currentNumber;
    public virtual void UpdateEffect(int value)
    {
        _currentNumber = value;
        ApplyEffect();
    }

    protected virtual void ApplyEffect() { }

}
