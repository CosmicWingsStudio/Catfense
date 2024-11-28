using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseGUIHandler : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _unpauseButton;
    [SerializeField] private GameObject _pauseScreenObject;
    private SignalBus _signalBus;

    [Inject]
    private void Initialize(SignalBus signalBus)
    {
        _signalBus = signalBus;


        _pauseButton.onClick.AddListener(OnPause);
        _unpauseButton.onClick.AddListener(OnUnpause);
    }

    private void OnPause()
    {
        _signalBus.Fire<PausedSignal>();
        _pauseScreenObject.SetActive(true);
    }

    private void OnUnpause()
    {
        _signalBus.Fire<UnpausedSignal>();
        _pauseScreenObject.SetActive(false);
    }
}
