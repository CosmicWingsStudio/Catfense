
using UnityEngine;

public class SoundMakerGUI : MonoBehaviour
{
    public static SoundMakerGUI Instance { get; private set; }

    private float volumeVar;
    public float Volume
    {
        get {return volumeVar; }
        set 
        {
            volumeVar = value;
            SetVolume();
        }
    }

    private AudioSource _audioSource;
    [Header("IF More than 1 audiosource")]
    [SerializeField] private AudioSource _subAudioSource;

    [Header("Audio clips")]
    #region AudioSource Fields
    public AudioClip SoundButtonClick;
    public AudioClip PurchaseButtonClick;
    public AudioClip BuildButtonClick;
    public AudioClip SoundDefaultPopUp;
    public AudioClip SoundErrorPopUp;
    public AudioClip SoundUnitPickedUp;
    public AudioClip SoundUnitPlacement;
    public AudioClip SoundWinResult;
    public AudioClip SoundLoseResult;
    public AudioClip SoundUpgradeUnit;
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

        _audioSource = GetComponent<AudioSource>();
    }

    private void SetVolume()
    {
        _audioSource.volume = Volume;
    }

    public void PlaySound(AudioClip sound)
    {
        _audioSource.clip = sound;
        _audioSource.Play();
    }
    public void PlaySoundInSubAudioSource(AudioClip sound)
    {
        if (_subAudioSource != null)
        {
            _subAudioSource.clip = sound;
            _subAudioSource.Play();
        }
    }
}
