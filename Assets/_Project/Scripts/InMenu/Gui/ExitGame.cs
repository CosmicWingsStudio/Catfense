using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    private Button _exitButton;

    private void Start()
    {
        _exitButton = GetComponent<Button>();

        _exitButton.onClick.AddListener(() => Application.Quit());
    }
}
