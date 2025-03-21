using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ADManager : MonoBehaviour
{
    [Header("For Menu")]
    [SerializeField] private Button _startADVideoForUnlockRealm;
    [SerializeField] private GameObject _adPanel;
    private int _currentIndex = 0;

    [Header("For Game")]
    [SerializeField] private Button _startADVideoForAdditionalGold;

    [Header("Common")]
    [SerializeField] private GameObject _adVideo;

    private SignalBus _signalBus;
    private ISaveService _saveService;
    private RealmsHandler _realmsHandler;

    [Inject]
    private void Initialize(ISaveService saveService, SignalBus signalBus, RealmsHandler realmsHandler)
    {
        _signalBus = signalBus;
        _saveService = saveService;
        _realmsHandler = realmsHandler;
    }

    private void Start()
    {
        if(_startADVideoForUnlockRealm != null)
            _startADVideoForUnlockRealm.onClick.AddListener(StartWatchingAD);

        if(_startADVideoForAdditionalGold != null)
            _startADVideoForAdditionalGold.onClick.AddListener(() => _adVideo.gameObject.SetActive(true));

        _signalBus.Subscribe<ADVideoEndedSignal>(UnlockRealm);
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
        _signalBus.Fire(new ADVideoStartedSignal(_currentIndex));
        ADObject ad = Instantiate(_adVideo).GetComponent<ADObject>();
        ad.Initialize(_signalBus);
    }

    private void UnlockRealm()
    {
        //change field in THE REALM
        _realmsHandler.GetRealms()[_currentIndex - 1].IsADWatched = true;
        //do save method
        _saveService.SaveData();

    }
}
