
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    private float MaxHealth;
    private float _originalMaxHealth;
    public float CurrentHealthPoint { get; protected set; }

    protected bool IsDead = false;
    protected Slider _healthPointSlider;
    protected ShowDamageText _damageText;

    private float _damageTimeDelay = 0.25f;
    private SpriteRenderer _spriteRenderer;
    private Color _defaultColor;

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

        if(GetComponent<EnemyUnit>())
            _spriteRenderer = GetComponent<SpriteRenderer>();
        else if(GetComponent<PlaceableUnit>())
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).TryGetComponent(out SpriteRenderer sr))
                {
                    _spriteRenderer = sr;
                    break;
                }
            }
        }
         
        if(_spriteRenderer != null)
            _defaultColor = _spriteRenderer.color;
    }

    public virtual void TakeDamage(float dmg)
    {
        if (IsDead)
            return;

        if(CurrentHealthPoint - dmg <= 0 == false)
        {
            CurrentHealthPoint -= dmg;
            StartCoroutine(DamageEffect());
            if (_damageText != null)
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

    protected void UpdateHealthPointsSlider()
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

    private IEnumerator DamageEffect()
    {
        float time = 0;
        float step = 1f / _damageTimeDelay;
        _spriteRenderer.color = Color.black;
        while (time < _damageTimeDelay)
        {
            time += Time.deltaTime;
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, _defaultColor, step * time);
            yield return null;
        }

    }
}
