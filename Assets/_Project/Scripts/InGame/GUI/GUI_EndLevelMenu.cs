using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GUI_EndLevelMenu : MonoBehaviour
{
    [Header("Outter")]
    [SerializeField] private Button _endLevelButton;

    [Header("Inner")]
    [SerializeField] private GameObject _endLevelMenu;
    [SerializeField] private Button _buttonLeave;
    [SerializeField] private Button _buttonStay;

    [Inject] private SignalBus _signalBus;


    private void Start()
    {
        _endLevelButton.onClick.AddListener(OpenMenu);
        _buttonStay.onClick.AddListener(CloseMenu);
        _buttonLeave.onClick.AddListener(EndGame);
    }

    private void OpenMenu()
    {
        _endLevelMenu.SetActive(true);
    }

    private void CloseMenu()
    {
        _endLevelMenu.SetActive(false);
    }

    private void EndGame()
    {
        CloseMenu();
        _endLevelButton.enabled = false;
        _signalBus.Fire(new LevelEndedSignal(ResultType.Lose));
    }
}
