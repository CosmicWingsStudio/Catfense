using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ADManagerForMenu : MonoBehaviour
{
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
        PauseDuringAD();
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

    private void PauseDuringAD()
    {
        _signalBus.Fire<PausedSignal>();
    }
}
