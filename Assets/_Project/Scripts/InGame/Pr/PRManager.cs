using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PRManager : MonoBehaviour
{
    [SerializeField] private Button _startPRVideo;
    [Inject] private SignalBus _signalBus;

    private void Start()
    {
        _startPRVideo.onClick.AddListener(() => _signalBus.Fire<PRVideoEndedSignal>());
    }


}
