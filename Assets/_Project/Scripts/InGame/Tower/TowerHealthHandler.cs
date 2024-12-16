using UnityEngine;
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

    protected override void Death()
    {
        base.Death();
    }

    public override void DeathAnimationPoint()
    {
        _signalBus.Fire(new LevelEndedSignal(ResultType.Lose));
    }
}
