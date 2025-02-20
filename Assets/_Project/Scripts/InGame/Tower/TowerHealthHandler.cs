
using Zenject;

public class TowerHealthHandler : HealthHandler
{
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
        if (IsDead)
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
}
