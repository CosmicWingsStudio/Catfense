using UnityEngine;
using Zenject;

public class GameModeSwitcher : MonoBehaviour
{
    private SignalBus _signalBus;
    private SceneEnemyFabric _sceneEnemyFabric; //ÕÇ ÍÀÄÎ ËÈ ÌÍÅ ÁÓÄÅÒ ÎÁÐÀÙÀÒÜÑß ÒÈÏÀ ËÀÑÒ ÝÍÈÌÈ ÇÄÎÕ È ÒÈÏ ÏÅÐÅÊËÞ×ÈÒÜ ÌÎÄ ÈËÈ ÊÀÊ?

    [SerializeField, Min(1)] private float _prepareTime = 25;

    private bool IsPrepareMode;
    private float _remainingTime = 0;

    [Inject]
    private void Initialize(SignalBus signalBus, SceneEnemyFabric sceneEnemyFabric)
    {
        _signalBus = signalBus;
        _sceneEnemyFabric = sceneEnemyFabric;

        IsPrepareMode = true;
    }

    private void Update()
    {
        if(IsPrepareMode)
        {
            if(_remainingTime + Time.deltaTime >= _prepareTime)
            {

            }
            else
            {

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
                _remainingTime = 0;
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