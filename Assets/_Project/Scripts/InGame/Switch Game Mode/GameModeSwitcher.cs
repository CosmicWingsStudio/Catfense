using UnityEngine;
using Zenject;

public class GameModeSwitcher : MonoBehaviour
{
    private SignalBus _signalBus;
    private SceneEnemyFactory _sceneEnemyFabric; //ÕÇ ÍÀÄÎ ËÈ ÌÍÅ ÁÓÄÅÒ ÎÁĞÀÙÀÒÜÑß ÒÈÏÀ ËÀÑÒ İÍÈÌÈ ÇÄÎÕ È ÒÈÏ ÏÅĞÅÊËŞ×ÈÒÜ ÌÎÄ ÈËÈ ÊÀÊ?

    [SerializeField, Min(1)] private float _prepareTime = 25;

    private bool IsPrepareMode;
    private bool IsPaused = false;
    private bool IsLevelEnded = false;

    private float _checkEnemyListTimer = 0f;
    public float RemainingTime { get; private set; }
    public bool IsEverlastingPreparationTimeByDevTools { get; set; }

    [Inject]
    private void Initialize(SignalBus signalBus, SceneEnemyFactory sceneEnemyFabric)
    {
        _signalBus = signalBus;
        _sceneEnemyFabric = sceneEnemyFabric;

        IsPrepareMode = true;
        RemainingTime = _prepareTime;

        _signalBus.Subscribe<PausedSignal>(() => IsPaused = true);
        _signalBus.Subscribe<UnpausedSignal>(() => IsPaused = false);
    }

    private void Update()
    {
        if (IsPaused || IsLevelEnded || IsEverlastingPreparationTimeByDevTools)
            return;

        if (IsPrepareMode)
        {
            if (RemainingTime - Time.deltaTime <= 0)
                SwitchMode(GameMode.WaveMode);
            else
                RemainingTime -= Time.deltaTime;
        }
        else
            CheckLastEnemyOnTheCurrentWave();

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

    private void CheckLastEnemyOnTheCurrentWave()
    {
        if (_sceneEnemyFabric.AllEnemyOnTheWaveIsSpawned)
        {
            if (_checkEnemyListTimer >= 1f) //íåáîëüøîé êóëäàóí, ÷òîá ñîêğàòèòü êîë-âî ïğîâåğîê ìàññèâà ñ âğàãàìè
            {
                _checkEnemyListTimer = 0;
                bool checkResult = _sceneEnemyFabric.CheckCurrentWaveEnemyListIsEmptylOrNot();

                if (checkResult && _sceneEnemyFabric.IsLastWave)
                {
                    IsLevelEnded = true;
                    _signalBus.Fire(new LevelEndedSignal(ResultType.Win));
                }
                else if (checkResult)
                    SwitchMode(GameMode.PrepareMode);
            }
            else
                _checkEnemyListTimer += Time.deltaTime;

        }
    }
}

public enum GameMode
{
    PrepareMode,
    WaveMode
}