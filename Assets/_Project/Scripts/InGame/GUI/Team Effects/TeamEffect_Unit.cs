
using UnityEngine;

public abstract class TeamEffect_Unit : UnityEngine.MonoBehaviour
{
    [SerializeField, Range(0.1f, 1f)] protected float _step1;
    [SerializeField, Range(0.1f, 1f)] protected float _step2;
    [SerializeField, Range(0.1f, 1f)] protected float _step3;

    protected int _currentNumber;
    public virtual void UpdateEffect(int value)
    {
        _currentNumber = value;
        ApplyEffect();
        UnityEngine.Debug.Log("Now I apply an effect as for " + _currentNumber + " units");
        _currentNumber = value + 1;
    }

    protected virtual void ApplyEffect() { }

}
