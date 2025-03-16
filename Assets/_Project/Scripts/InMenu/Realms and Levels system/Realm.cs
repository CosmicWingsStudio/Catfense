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

    [SerializeField] private Button OpenRealmButton;
    [SerializeField] private GameObject DisablePanel;
    [SerializeField] private Toggle IsCompletedToggle;

    public RealmLevelsHandler realmLevelsHandler;

    private void Awake()
    {
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
}
