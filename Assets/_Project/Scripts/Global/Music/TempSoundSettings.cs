
using UnityEngine;

public class TempSoundSettings : MonoBehaviour
{
    public static TempSoundSettings Instance { get; private set; }

    public VolumeSettings VolumeSettings { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
        SetDefaultValues();
    }


    public void UpdateVolumeSettings(float musv, float guiv, float masv, float unv)
    {
        VolumeSettings.MusicVolume = musv;
        VolumeSettings.GUIVolume = guiv;
        VolumeSettings.MasterVolume = masv;
        VolumeSettings.UnitVolume = unv;   
    }

    private void SetDefaultValues()
    {
        VolumeSettings = new(0.55f, 0.55f, 1f, 0.5f);
    }
}
