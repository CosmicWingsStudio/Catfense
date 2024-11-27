using UnityEngine;
using Zenject;

public class GameModeSwitcher : MonoBehaviour
{
    private SignalBus _signalBus;
    private SceneEnemyFactory _sceneEnemyFabric; //ХЗ НАДО ЛИ МНЕ БУДЕТ ОБРАЩАТЬСЯ ТИПА ЛАСТ ЭНИМИ ЗДОХ И ТИП ПЕРЕКЛЮЧИТЬ МОД ИЛИ КАК?

    [SerializeField, Min(1)] private float _prepareTime = 25;

    private bool IsPrepareMode;

    private float _checkEnemyListTimer = 0f;
    public float RemainingTime { get; private set; }

    [Inject]
    private void Initialize(SignalBus signalBus, SceneEnemyFactory sceneEnemyFabric)
    {
        _signalBus = signalBus;
        _sceneEnemyFabric = sceneEnemyFabric;

        IsPrepareMode = true;
        RemainingTime = _prepareTime;
    }

    private void Update()
    {
        if(IsPrepareMode)
        {
            if (RemainingTime - Time.deltaTime <= 0)
            {
                SwitchMode(GameMode.WaveMode);
            }
            else
                RemainingTime -= Time.deltaTime;
        }
        else
        {
            if(_sceneEnemyFabric.AllEnemyOnTheWaveIsSpawned)
            {
                if (_checkEnemyListTimer >= 1f) //небольшой кулдаун, чтоб сократить кол-во проверок массива с врагами
                {
                    _checkEnemyListTimer = 0;
                    if(_sceneEnemyFabric.CheckCurrentWaveEnemyListIsEmptylOrNot())
                        SwitchMode(GameMode.PrepareMode);
                }
                else
                    _checkEnemyListTimer += Time.deltaTime;
                
            }
        }
    }

    private void SwitchMode(GameMode Mode)
    {
        switch (Mode)
        {
            case GameMode.PrepareMode:  
                IsPrepareMode = true;
                _signalBus.Fire<WaveEndedSignal>();
                break;
            case GameMode.WaveMode:
                RemainingTime = _prepareTime;
                IsPrepareMode = false;
                _signalBus.Fire<WaveStartedSignal>();
                break;

        }
    }
}

public enum GameMode
{
    PrepareMode,
    WaveMode
}