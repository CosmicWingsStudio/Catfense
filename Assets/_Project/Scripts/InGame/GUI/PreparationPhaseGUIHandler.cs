using TMPro;
using UnityEngine;
using Zenject;

public class PreparationPhaseGUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject _guiPreparationFolder;
    [SerializeField] private TextMeshProUGUI _preparationTimerText;

    private bool IsPrepareMode;
    private GameModeSwitcher _gameModeSwitcher;
    private SignalBus _signalBus;

    [Inject]
    private void Initialize(GameModeSwitcher gameModeSwitcher, SignalBus signalBus)
    {
        _gameModeSwitcher = gameModeSwitcher;
        _signalBus = signalBus;

        IsPrepareMode = true;
        _guiPreparationFolder.SetActive(true);
        _signalBus.Subscribe<WaveStartedSignal>(TurnOffPrepatationGUI);
        _signalBus.Subscribe<WaveEndedSignal>(TurnOnPrepatationGUI);
    }

    private void Update()
    {
        if (IsPrepareMode)
        {
            int timer = (int)_gameModeSwitcher.RemainingTime;
            _preparationTimerText.text = timer.ToString();
        }
    }

    private void TurnOffPrepatationGUI()
    {
        _guiPreparationFolder.SetActive(false);
        IsPrepareMode = false;
    }

    private void TurnOnPrepatationGUI()
    {
        _guiPreparationFolder.SetActive(true);
        IsPrepareMode = true;
    }
}
