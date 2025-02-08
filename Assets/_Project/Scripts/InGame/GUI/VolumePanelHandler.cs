using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class VolumePanelHandler : MonoBehaviour
{
    //[Inject] private SoundHandler _soundHandler;

    [SerializeField] private Button _volumeButton;
    [SerializeField] private GameObject _volumePanel;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _otherSlider;


    private bool IsOpened = false;

    private void Start()
    {
        _volumePanel.SetActive(false);
        _volumeButton.onClick.AddListener(SwitchVisibility);
        //_musicSlider.value = _soundHandler.MusicVolume;
        //_otherSlider.value = _soundHandler.OtherVolume;
        //_musicSlider.onValueChanged.AddListener(() => _soundHandler.MusicVolume);
        //_otherSlider.onValueChanged.AddListener(() => _soundHandler.OtherVolume);
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
