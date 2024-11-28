using TMPro;
using UnityEngine;
using Zenject;

public class ResultScreenGUIHandler : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;

    [Header("Put references")]

    [SerializeField] private GameObject _resultScreenObject;
    [SerializeField] private TextMeshProUGUI _resultText;

    [Header("Put string values")]

    [SerializeField] private string _winResultInscription = "Уровень пройден";
    [SerializeField] private string _loseResultInscription = "Уровень не пройден";

    private void Start() => _signalBus.Subscribe<LevelEndedSignal>(ShowResultScreen);
    
    public void ShowResultScreen(LevelEndedSignal levelEndedSignal)
    {
        switch (levelEndedSignal.ResultType)
        {
            case ResultType.Win:
                _resultScreenObject.SetActive(true);
                _resultText.text = _winResultInscription;
                break;
            case ResultType.Lose:
                _resultScreenObject.SetActive(true);
                _resultText.text = _loseResultInscription;
                break;

        }
    }
}

public enum ResultType
{
    Win,
    Lose
}