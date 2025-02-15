using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class MakerSoundOnClick : MonoBehaviour
{
    [SerializeField] private bool _defaultClick = true;
    [SerializeField] private bool _purchaseClick = false;
    [SerializeField] private bool _buildClick = false;

    private void Start()
    {
        if (_defaultClick)
        {
            GetComponent<UnityEngine.UI.Button>().onClick.AddListener
            (
                () => SoundMakerGUI.Instance.PlaySound(SoundMakerGUI.Instance.SoundButtonClick)
            );
        }
        else if(_purchaseClick)
        {
            GetComponent<UnityEngine.UI.Button>().onClick.AddListener
            (
                () => SoundMakerGUI.Instance.PlaySound(SoundMakerGUI.Instance.PurchaseButtonClick)
            );
        }
        else
        {
            GetComponent<UnityEngine.UI.Button>().onClick.AddListener
            (
                () => SoundMakerGUI.Instance.PlaySound(SoundMakerGUI.Instance.BuildButtonClick)
            );
        }
        
    }

    private void OnValidate()
    {
        if (_defaultClick)
        {
            _purchaseClick = false;
            _buildClick = false;
        }
        else
        {
            if (_purchaseClick)
                _buildClick = false;
            else
                _buildClick = true;
        }
        
    }
}
