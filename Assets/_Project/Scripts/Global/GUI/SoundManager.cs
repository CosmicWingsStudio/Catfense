using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider _guiVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _masterVolumeSlider;

    [SerializeField] private AudioMixer _audioMixer;

    private float _beforeSavingTreshold = 3f;
    private float _beforeSavingTresholdTimer;
    private bool VolumeDataTouched = false;

    private void Update()
    {
        if (VolumeDataTouched)
        {
            _beforeSavingTresholdTimer += Time.deltaTime;
            if(_beforeSavingTresholdTimer >= _beforeSavingTreshold)
            {
                SavePrefs();
                _beforeSavingTresholdTimer = 0f;
                VolumeDataTouched = false;
            }
        }
    }

    private void Start()
    {
        LoadSavedPrefs();

        _guiVolumeSlider.onValueChanged.AddListener(value => SetGUIVolume(value));
        _musicVolumeSlider.onValueChanged.AddListener(value => SetMusicVolume(value));
        _masterVolumeSlider.onValueChanged.AddListener(value => SetMasterVolume(value));
    }

    private void LoadSavedPrefs()
    {
        try
        {
            VolumeSettings savedVS = JsonUtility.FromJson<VolumeSettings>(PlayerPrefs.GetString("VolumeSettings"));

            _musicVolumeSlider.value = savedVS.MusicVolume;
            _guiVolumeSlider.value = savedVS.GUIVolume;
            _masterVolumeSlider.value = savedVS.MasterVolume;

            _audioMixer.SetFloat("musicVolume", Mathf.Log10(_musicVolumeSlider.value) * 20);
            _audioMixer.SetFloat("guiVolume", Mathf.Log10(_guiVolumeSlider.value) * 20);
            _audioMixer.SetFloat("masterVolume", Mathf.Log10(_masterVolumeSlider.value) * 20);
        }
        catch (System.Exception)
        {
            SavePrefs();
        }
           
    }

    private void SavePrefs()
    {
        VolumeSettings vs = new
        (
             _musicVolumeSlider.value,
             _guiVolumeSlider.value,
             _masterVolumeSlider.value
        );

        PlayerPrefs.SetString("VolumeSettings", JsonUtility.ToJson(vs));
        PlayerPrefs.Save();
    }

    private void SetGUIVolume(float guiVolume)
    {
        _audioMixer.SetFloat("guiVolume", Mathf.Log10(guiVolume) * 20);
        VolumeDataTouched = true;
        _beforeSavingTresholdTimer = 0f;
    }

    private void SetMusicVolume(float musicVolume)
    {
        _audioMixer.SetFloat("musicVolume", Mathf.Log10(musicVolume) * 20);
        VolumeDataTouched = true;
        _beforeSavingTresholdTimer = 0f;
    }

    private void SetMasterVolume(float masterVolume)
    {
        _audioMixer.SetFloat("masterVolume", Mathf.Log10(masterVolume) * 20);
        VolumeDataTouched = true;
        _beforeSavingTresholdTimer = 0f;
    }

    //set settings

    //save settings

}
