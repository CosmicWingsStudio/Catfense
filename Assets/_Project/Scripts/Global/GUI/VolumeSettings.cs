using System;

[Serializable]
public class VolumeSettings
{
    public VolumeSettings(float musv, float guiv, float masv)
    {
        MusicVolume = musv;
        GUIVolume = guiv;
        MasterVolume = masv;
    }

    public float MusicVolume;
    public float GUIVolume;
    public float MasterVolume;

}
