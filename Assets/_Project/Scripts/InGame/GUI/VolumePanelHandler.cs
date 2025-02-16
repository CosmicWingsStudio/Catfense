using UnityEngine;
using UnityEngine.UI;

public class VolumePanelHandler : MonoBehaviour
{
    //[Inject] private SoundHandler _soundHandler;

    [SerializeField] private Button _volumeButton;
    [SerializeField] private GameObject _volumePanel;

    private bool IsOpened = false;

    private void Start()
    {
        _volumePanel.SetActive(false);
        _volumeButton.onClick.AddListener(SwitchVisibility); 
    }

    private void SwitchVisibility()
    {
        if(!IsOpened)
        {
            _volumePanel.SetActive(true);
            IsOpened = true;
        }
        else
        {
            _volumePanel.SetActive(false);
            IsOpened = false;
        }
    }
}
