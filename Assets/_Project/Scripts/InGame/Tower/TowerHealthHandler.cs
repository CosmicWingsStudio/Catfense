
using System.Collections;
using UnityEngine;
using Zenject;


public class TowerHealthHandler : HealthHandler
{
    [SerializeField] private Transform _defendEffect;
    private SignalBus _signalBus;
    protected Tower _tower;

    public void SetSignalBus(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Start()
    {
        _tower = GetComponent<Tower>();

        SetHealthParams(_tower.Health, _tower.HealthPointsSlider);
    }

    public override void TakeDamage(float dmg)
    {
        if (IsDead || IsInvincible)
            return;

        if (CurrentHealthPoint - dmg <= 0 == false)
        {
            CurrentHealthPoint -= dmg;
            UpdateHealthPointsSlider();
        }
        else
        {
            _healthPointSlider.value = 0f;
            Death();
        }
    }

    protected override void Death()
    {
        base.Death();
    }

    public override void DeathAnimationPoint()
    {
        _signalBus.Fire(new LevelEndedSignal(ResultType.Lose));
    }

    public void SetDefendEffect(float time)
    {
        SetInvincibleEffect(time);
        _defendEffect.gameObject.SetActive(true);
    }

    protected override IEnumerator DefendEffect(float time)
    {
        yield return new WaitForSeconds(time);
        IsInvincible = false;
        _defendEffect.gameObject.SetActive(false);
    }
}
