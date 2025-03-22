using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ADManagerForGame : MonoBehaviour
{
    [SerializeField] private Button _startADVideoForAdditionalGold;
    [SerializeField] private GameObject _adVideo;

    [Inject] readonly SignalBus _signalBus;

    private void Start()
    {
        _startADVideoForAdditionalGold.onClick.AddListener(StartWatchingAD);
    }

    private void StartWatchingAD()
    {
        Time.timeScale = 1.0f;
        ADObject ad = Instantiate(_adVideo).GetComponent<ADObject>();
        ad.Initialize(_signalBus);
    }
}
