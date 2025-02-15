using UnityEngine;

public class SoundMakerMusic : MonoBehaviour
{
    public static SoundMakerMusic Instance { get; private set; }

    private float volumeVar;
    public float Volume
    {
        get { return volumeVar; }
        set
        {
            volumeVar = value;
            SetVolume();
        }
    }

    #region AudioSource Fields
    [SerializeField] private AudioSource _buttonClick;


    #endregion

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


    }

    private void SetVolume()
    {
        _buttonClick.volume = Volume;
    }

    //change music

    //randomise
}
