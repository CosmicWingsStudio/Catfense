using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider _guiVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _unitVolumeSlider;

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private bool IsMenuVersion = false;

    private float _beforeSavingTreshold = 3f;
    private float _beforeSavingTresholdTimer;
    private bool VolumeDataTouched = false;
    private bool IsUnfocused = false;
    private VolumeSettings _hashedVolumeSettings;

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
        _unitVolumeSlider.onValueChanged.AddListener(value => SetUnitVolume(value));
        
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            if(IsUnfocused == false)
            {

                VolumeSettings vlm = TempSoundSettings.Instance.VolumeSettings;
                _hashedVolumeSettings = new(vlm.MusicVolume, vlm.GUIVolume, vlm.MasterVolume, vlm.UnitVolume);
                IsUnfocused = true;

                TempSoundSettings.Instance.UpdateVolumeSettings(0f,
                0f,
                0f,
                0f);

                VolumeSettings vs = TempSoundSettings.Instance.VolumeSettings;

                _musicVolumeSlider.value = vs.MusicVolume;
                _guiVolumeSlider.value = vs.GUIVolume;
                _masterVolumeSlider.value = vs.MasterVolume;
                _unitVolumeSlider.value = vs.UnitVolume;

                _audioMixer.SetFloat("musicVolume", Mathf.Log10(_musicVolumeSlider.value) * 20);
                _audioMixer.SetFloat("guiVolume", Mathf.Log10(_guiVolumeSlider.value) * 20);
                _audioMixer.SetFloat("masterVolume", Mathf.Log10(_masterVolumeSlider.value) * 20);
                _audioMixer.SetFloat("unitVolume", Mathf.Log10(_unitVolumeSlider.value) * 20);
            }
            

        }
        else if(focus && IsUnfocused)
        {

            IsUnfocused = false;
            TempSoundSettings.Instance.UpdateVolumeSettings(_hashedVolumeSettings.MusicVolume, _hashedVolumeSettings.GUIVolume,
            _hashedVolumeSettings.MasterVolume,
            _hashedVolumeSettings.UnitVolume);

            VolumeSettings vs = _hashedVolumeSettings;

            _musicVolumeSlider.value = vs.MusicVolume;
            _guiVolumeSlider.value = vs.GUIVolume;
            _masterVolumeSlider.value = vs.MasterVolume;
            _unitVolumeSlider.value = vs.UnitVolume;

            _audioMixer.SetFloat("musicVolume", Mathf.Log10(_musicVolumeSlider.value) * 20);
            _audioMixer.SetFloat("guiVolume", Mathf.Log10(_guiVolumeSlider.value) * 20);
            _audioMixer.SetFloat("masterVolume", Mathf.Log10(_masterVolumeSlider.value) * 20);
            _audioMixer.SetFloat("unitVolume", Mathf.Log10(_unitVolumeSlider.value) * 20);
        }
    }

    private void LoadSavedPrefs()
    {
        //try
        //{
        //    VolumeSettings savedVS = JsonUtility.FromJson<VolumeSettings>(PlayerPrefs.GetString("VolumeSettings"));

        //    _musicVolumeSlider.value = savedVS.MusicVolume;
        //    _guiVolumeSlider.value = savedVS.GUIVolume;
        //    _masterVolumeSlider.value = savedVS.MasterVolume;
        //    _unitVolumeSlider.value = savedVS.UnitVolume;

        //    _audioMixer.SetFloat("musicVolume", Mathf.Log10(_musicVolumeSlider.value) * 20);
        //    _audioMixer.SetFloat("guiVolume", Mathf.Log10(_guiVolumeSlider.value) * 20);
        //    _audioMixer.SetFloat("masterVolume", Mathf.Log10(_masterVolumeSlider.value) * 20);
        //    _audioMixer.SetFloat("unitVolume", Mathf.Log10(_unitVolumeSlider.value) * 20);
        //}
        //catch (System.Exception)
        //{
        //    SavePrefs();
        //}

        VolumeSettings vs = TempSoundSettings.Instance.VolumeSettings;

        _musicVolumeSlider.value = vs.MusicVolume;
        _guiVolumeSlider.value = vs.GUIVolume;
        _masterVolumeSlider.value = vs.MasterVolume;
        _unitVolumeSlider.value = vs.UnitVolume;

        _audioMixer.SetFloat("musicVolume", Mathf.Log10(_musicVolumeSlider.value) * 20);
        _audioMixer.SetFloat("guiVolume", Mathf.Log10(_guiVolumeSlider.value) * 20);
        _audioMixer.SetFloat("masterVolume", Mathf.Log10(_masterVolumeSlider.value) * 20);
        _audioMixer.SetFloat("unitVolume", Mathf.Log10(_unitVolumeSlider.value) * 20);

    }

    private void SavePrefs()
    {
        TempSoundSettings.Instance.UpdateVolumeSettings(_musicVolumeSlider.value,
             _guiVolumeSlider.value,
             _masterVolumeSlider.value,
             _unitVolumeSlider.value);
       
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

    private void SetUnitVolume(float masterVolume)
    {
        _audioMixer.SetFloat("unitVolume", Mathf.Log10(masterVolume) * 20);
        VolumeDataTouched = true;
        _beforeSavingTresholdTimer = 0f;
    }

    //set settings

    //save settings

}
