using UnityEngine;
using UnityEngine.UI;
using Zenject;
using YG;

public class ADManagerForGame : MonoBehaviour
{
    private const int GetMoneyAdId = 1; 

    [SerializeField] private Button _startADVideoForAdditionalGold;
    [SerializeField] private GameObject _adVideo;

    [Inject] readonly SignalBus _signalBus;

    private void Start()
    {
        _startADVideoForAdditionalGold.onClick.AddListener(StartWatchingAD);
        YandexGame.RewardVideoEvent += GetReward;
    }

    private void StartWatchingAD()
    {
        Time.timeScale = 1.0f;
        YandexGame.RewVideoShow(GetMoneyAdId);
    }

    private void GetReward(int id)
    {
        if (id != GetMoneyAdId)
            return;

        _signalBus.Fire<ADVideoEndedSignal>();
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= GetReward;
    }
}
