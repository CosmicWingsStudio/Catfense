using TMPro;
using UnityEngine;

public class GUIWarningHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _causeText;
    [SerializeField] private float _defaultDisappearDelay = 2.5f;
    
    private GameObject _object;
    private bool IsActive = false;
    private float _delayTimer = 0f;

    private void Start()
    {
        _object = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (IsActive)
        {
            if (_delayTimer >= _defaultDisappearDelay)
            {
                CloseWarningScreen();
                _delayTimer = 0f;
            }
            else
                _delayTimer += Time.deltaTime;
        }
    }

    public void ShowWarningScreen(string causeText)
    {
        _object.SetActive(true);
        _object.transform.position = Input.mousePosition;

        if(_object.TryGetComponent(out MakerSoundOnPopUp soundComp))
        {
            soundComp.PlayCurrentPopUpSound();
        }

        IsActive = true;
        _causeText.text = causeText;
    }

    private void CloseWarningScreen()
    {
        IsActive = false;
        _object.SetActive(false);   
    }
}
