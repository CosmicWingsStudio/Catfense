using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

public class ADManagerForMenu : MonoBehaviour
{
    private const int GetMoneyAdId = 2;

    [SerializeField] private Button _startADVideoForUnlockRealm;
    [SerializeField] private GameObject _adPanel;
    [SerializeField] private GameObject _adVideo;

    private SignalBus _signalBus;
    private ISaveService _saveService;
    private RealmsHandler _realmsHandler;
    private int _currentIndex = 0;

    [Inject]
    private void Initialize(ISaveService saveService, SignalBus signalBus, RealmsHandler realmsHandler)
    {
        _signalBus = signalBus;
        _saveService = saveService;
        _realmsHandler = realmsHandler;
    }

    private void Start()
    {
        _startADVideoForUnlockRealm.onClick.AddListener(StartWatchingAD);
        YandexGame.RewardVideoEvent += UnlockRealm;
    }

    public void ShowADPanelFromRealmButton(int realmIndex, Transform pos)
    {
        _currentIndex = realmIndex;
        _adPanel.transform.position = pos.transform.position;
        _adPanel.SetActive(true);
    }

    public void CloseADPanel()
    {
        _currentIndex = 0;
        _adPanel.SetActive(false);
    }

    private void StartWatchingAD()
    {
        _adPanel.gameObject.SetActive(false);
        PauseDuringAD();
        YandexGame.RewVideoShow(GetMoneyAdId);
    }

    private void UnlockRealm(int id)
    {
        if (id != GetMoneyAdId)
            return;

        _realmsHandler.GetRealms()[_currentIndex - 1].IsADWatched = true;
        _saveService.SaveData();

    }

    private void PauseDuringAD()
    {
        _signalBus.Fire<PausedSignal>();
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= UnlockRealm;
    }
}
