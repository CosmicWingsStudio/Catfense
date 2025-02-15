using UnityEngine;

public class MakerSoundOnPopUp : MonoBehaviour
{

    [SerializeField] private bool _defaultPopUp = true;
    [SerializeField] private bool _errorPopUp = false;
    [SerializeField] private bool NoSoundOnEnable = false;

    private void OnEnable()
    {
        if (NoSoundOnEnable == false)
            PlayCurrentPopUpSound();
        
    }

    private void OnValidate()
    {
        if (_defaultPopUp)
            _errorPopUp = false;
        else
            _errorPopUp = true;
    }

    public void PlayCurrentPopUpSound()
    {
        if (_defaultPopUp)
        {
            SoundMakerGUI.Instance.PlaySound(SoundMakerGUI.Instance.SoundDefaultPopUp);
        }
        else
        {
            SoundMakerGUI.Instance.PlaySound(SoundMakerGUI.Instance.SoundErrorPopUp);
        }
    }
}
