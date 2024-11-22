using UnityEngine;
using UnityEngine.UI;

public class EnableMenuButton : MonoBehaviour
{
    [SerializeField] private GameObject _objectToEnable;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();

        if(_button != null)       
            _button.onClick.AddListener(() => _objectToEnable.SetActive(true));
        
    }
}
