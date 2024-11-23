using UnityEngine;
using UnityEngine.UI;

public class DisableMenuButton : MonoBehaviour
{
    [SerializeField] private GameObject _objectToDisable;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();

        if (_button != null)
            _button.onClick.AddListener(() => _objectToDisable.SetActive(false));

    }
}
