using System;

[Serializable]
public class VolumeSettings
{
    public VolumeSettings(float musv, float guiv, float masv, float unv)
    {
        MusicVolume = musv;
        GUIVolume = guiv;
        MasterVolume = masv;
        UnitVolume = unv;
    }

    public float MusicVolume;
    public float GUIVolume;
    public float MasterVolume;
    public float UnitVolume;

}
