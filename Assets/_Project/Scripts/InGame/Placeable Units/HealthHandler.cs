

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
    private Animator _animator;

    public void SetHealthParams(int maxHealth, Slider hpSlider)
    {
        MaxHealth = maxHealth;
        _originalMaxHealth = MaxHealth;
        _healthPointSlider = hpSlider;
        CurrentHealthPoint = MaxHealth;
        _animator = GetComponent<Animator>();
        if (TryGetComponent(out ShowDamageText showDamageText))
        {
            _damageText = showDamageText;
        }

        hpSlider.value = 1f;  
    }

    public virtual void TakeDamage(float dmg)
    {
        if (IsDead)
            return;

        if(CurrentHealthPoint - dmg <= 0 == false)
        {
            CurrentHealthPoint -= dmg;
            if (_damageText != null)
                _damageText.ShowDamage(dmg);
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
        if (GetComponent<EnemyUnit>())
        {
            _animator.SetBool("IsDying", true);
            _animator.SetTrigger("Death");
            float randomValue = Random.Range(0, 100);
            if (randomValue <= 10)
            {
                GetComponent<EnemyUnit>().RewardSpawner.SpawnReward(Camera.main.WorldToScreenPoint(transform.position));
            }
            
        }
        else
        {
            DeathAnimationPoint();
            Vector3 efxPos = transform.position;
            //efxPos.y += 1.5f;
            FxSpawner.Instance.SpawnDeathEffect(efxPos);
        }
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
}
