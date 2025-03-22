using UnityEngine;
using UnityEngine.UI;

public class Realm : MonoBehaviour
{
    private bool _isCompleted;
    public bool IsCompleted
    {
        get { return _isCompleted; }
        set => MakeRealmCompleted();
    }


    [ReadOnly] public bool IsAvaliable = false;
    [ReadOnly] public bool IsADWatched = false;
    [SerializeField] private int _realmNumber = 0;
    [SerializeField] private Button OpenRealmButton;
    [SerializeField] private GameObject ObjectToOpen;
    [SerializeField] private bool HasADVideo = false;
    [SerializeField] private GameObject DisablePanel;
    [SerializeField] private Toggle IsCompletedToggle;
    [SerializeField] private ADManagerForMenu adManager;

    public RealmLevelsHandler realmLevelsHandler;

    private void Awake()
    {
        OpenRealmButton.onClick.AddListener(OpenRealmPanel);
        OpenRealmButton.enabled = false;
        IsCompletedToggle.isOn = IsCompleted;

    }
    public void MakeRealmAvailable()
    {
        IsAvaliable = true;
        OpenRealmButton.enabled = true;
        DisablePanel.SetActive(false);
    }

    public void MakeRealmCompleted()
    {
        _isCompleted = true;
        IsCompletedToggle.isOn = _isCompleted;
    }

    private void OpenRealmPanel()
    {
        if(HasADVideo == true && IsADWatched == false)
        {
            adManager.ShowADPanelFromRealmButton(_realmNumber, transform);
        }
        else
            ObjectToOpen.SetActive(true);
    }
}
