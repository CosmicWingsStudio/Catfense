using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PreparationPhaseGUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject _guiPreparationTimer;
    [SerializeField] private GameObject _guiPreparationFirstWave;
    [SerializeField] private Button _startFirstWaveButton;
    [SerializeField] private TextMeshProUGUI _preparationTimerText;
    [SerializeField] private int _requiredAmountOfUnits = 1;

    private bool IsPrepareMode;
    private bool IsFirstWaveEnded = false;
    private GameModeSwitcher _gameModeSwitcher;
    private SignalBus _signalBus;
    private EnvironmentHandler _environmentHandler;
    private EnvironmentContainerHandler _environmentContainer;

    [Inject]
    private void Initialize(GameModeSwitcher gameModeSwitcher, SignalBus signalBus, EnvironmentHandler envHadler)
    {
        _gameModeSwitcher = gameModeSwitcher;
        _signalBus = signalBus;
        _environmentHandler = envHadler;

        IsPrepareMode = true;
        _guiPreparationFirstWave.SetActive(true);

        _startFirstWaveButton.onClick.AddListener(StartFirstWave);
        _signalBus.Subscribe<WaveStartedSignal>(TurnOffPrepatationGUI);
        _signalBus.Subscribe<WaveEndedSignal>(TurnOnPrepatationGUI);
    }

    private void Update()
    {
        if (IsFirstWaveEnded && IsPrepareMode)
        {
            int timer = (int)_gameModeSwitcher.RemainingTime;
            _preparationTimerText.text = timer.ToString();
        }
    }

    private void StartFirstWave()
    {
        if (CheckStartWaveConditions())
        {
            _gameModeSwitcher.StartFirstWave();
            _guiPreparationFirstWave.SetActive(false);
        }
    }

    private bool CheckStartWaveConditions()
    {
        _environmentContainer = _environmentHandler.GetEnvironmentContainer();
        int amount = 0;

        for (int i = 0; i < _environmentContainer.PlaceSlots.Count; i++)
        {
            if (_environmentContainer.PlaceSlots[i].Item != null)
            {
                amount++;
                if(amount == _requiredAmountOfUnits)
                    return true;
            }
        }

        return false;
    }

    private void TurnOffPrepatationGUI()
    {
        _guiPreparationTimer.SetActive(false);
        IsPrepareMode = false;
    }

    private void TurnOnPrepatationGUI()
    {
        if (!IsFirstWaveEnded)
            ActionAfterFirstWave();
        else
        {
            _guiPreparationTimer.SetActive(true);
            IsPrepareMode = true;
        }
    }

    private void ActionAfterFirstWave()
    {
        _guiPreparationTimer.SetActive(true);
        IsFirstWaveEnded = true;
        IsPrepareMode = true;
    }
}
