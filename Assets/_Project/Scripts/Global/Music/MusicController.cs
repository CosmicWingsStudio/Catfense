using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;

    [SerializeField] private AudioClip[] _musicClips;

    [SerializeField]
    private bool MainMenuVersion = false;

    private List<AudioClip> _currentMusicPlaylist = new();
    private AudioClip _currentAudioClip;
    private AudioSource _audioSource;
    private bool IsPaused = false;
    private bool IsInitialized = false;
    private float _cooldownTime = 1f;
    private float _cooldownTimer = 0f;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (MainMenuVersion)
        {   
            FillCurrentPlaylist();
            PlayMusic();
        }  
    }

    public void SetData(AudioClip[] ConfigMusicClips)
    {
        if (IsInitialized && MainMenuVersion)
            return;

        _signalBus.Subscribe<PausedSignal>(Pause);
        _signalBus.Subscribe<UnpausedSignal>(UnPause);
        _signalBus.Subscribe<LevelEndedSignal>(Pause);

        _audioSource = GetComponent<AudioSource>();

        _musicClips = new AudioClip[ConfigMusicClips.Length];

        for (int i = 0; i < ConfigMusicClips.Length; i++)
        {
            _musicClips[i] = ConfigMusicClips[i];
        }

        FillCurrentPlaylist();
        PlayMusic();

        IsInitialized = true;
    }

    private void Update()
    {
        if(_cooldownTimer < _cooldownTime)
        {
            _cooldownTimer += Time.deltaTime;
            return;
        }
        if(!IsPaused && !_audioSource.isPlaying && IsInitialized && Application.isFocused)
        {
            PlayMusic();
            _cooldownTimer = 0f;
        }
    }

    private void PlayMusic()
    {
        if (_currentMusicPlaylist.Count < 1)
            FillCurrentPlaylist();
        
        int randomisedIter = RandomizeMusic();
        _currentAudioClip = _currentMusicPlaylist[randomisedIter];
        _audioSource.clip = _currentAudioClip;
        _audioSource.Play();

        _currentMusicPlaylist.RemoveAt(randomisedIter);
    }

    private int RandomizeMusic()
    {
        return Random.Range(0, _musicClips.Length);
    }

    private void FillCurrentPlaylist()
    {
        for (int i = 0; i < _musicClips.Length; i++)
        {
            _currentMusicPlaylist.Add(_musicClips[i]);
        }
    }

    private void Pause()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Pause();
            IsPaused = true;
        }
    }

    private void UnPause()
    {
        _audioSource.UnPause();
        IsPaused = false;
    }
}
