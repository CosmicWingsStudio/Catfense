
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    private float MaxHealth;
    private float _originalMaxHealth;
    public float CurrentHealthPoint { get; private set; }

    protected bool IsDead = false;
    protected Slider _healthPointSlider;
    protected ShowDamageText _damageText;

    public void SetHealthParams(int maxHealth, Slider hpSlider)
    {
        MaxHealth = maxHealth;
        _originalMaxHealth = MaxHealth;
        _healthPointSlider = hpSlider;
        CurrentHealthPoint = MaxHealth;
        if(TryGetComponent(out ShowDamageText showDamageText))
        {
            _damageText = showDamageText;
        }

        hpSlider.value = 1f;
    }

    public void TakeDamage(float dmg)
    {
        if (IsDead)
            return;

        if(CurrentHealthPoint - dmg <= 0 == false)
        {
            CurrentHealthPoint -= dmg;

            //чернение спрайта
            if(_damageText != null)
                _damageText.ShowDamage(dmg);
            //звук
            UpdateHealthPointsSlider();
        }
        else
        {
            _healthPointSlider.value = 0f;
            Death();
        }
    }

    public void Heal(int healAmount)
    {
        if (IsDead)
            return;

        if (CurrentHealthPoint + healAmount > MaxHealth == false)
        {
            CurrentHealthPoint += healAmount;

        }
        else
        {
            CurrentHealthPoint = MaxHealth;
        }

        UpdateHealthPointsSlider();
    }

    protected virtual void Death()
    {
        IsDead = true;
        //start animation

        //временно
        DeathAnimationPoint();
    }

    public virtual void DeathAnimationPoint()
    {
        if (IsDead)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateHealthPointsSlider()
    {
        float newValue = (100 / (MaxHealth / CurrentHealthPoint)) / 100;
        if (newValue <= 0 == false)
            _healthPointSlider.value = newValue;
        else
            _healthPointSlider.value = 0f;
    }

    public void UpgradeStats(int multiplier)
    {
        MaxHealth += multiplier * (_originalMaxHealth / 10);
        CurrentHealthPoint = MaxHealth;
        UpdateHealthPointsSlider();
    }
}
