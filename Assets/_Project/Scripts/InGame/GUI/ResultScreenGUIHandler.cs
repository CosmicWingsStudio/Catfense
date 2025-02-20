using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class ResultScreenGUIHandler : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;
    [Inject] private ISaveService _saveService;
    private int _realmIndex;
    private int _levelIndex;

    [Header("Put references")]

    [SerializeField] private GameObject _resultScreenObject;
    [SerializeField] private TextMeshProUGUI _resultText;
    [SerializeField] private Button _exitButton;
 
    [Header("Put string values")]

    [SerializeField] private string _winResultInscription = "Уровень пройден";
    [SerializeField] private string _loseResultInscription = "Уровень не пройден";

    private void Start()
    {
        _signalBus.Subscribe<LevelEndedSignal>(ShowResultScreen);
        _exitButton.onClick.AddListener(() => SceneManager.LoadScene("InMenuScene"));
    } 
    
    public void ShowResultScreen(LevelEndedSignal levelEndedSignal)
    {
        switch (levelEndedSignal.ResultType)
        {
            case ResultType.Win:
                Debug.Log("SAVE RESULT IS OFF");
                //SaveResult();
                _resultScreenObject.SetActive(true);
                _resultText.text = _winResultInscription;
                SoundMakerGUI.Instance.PlaySound(SoundMakerGUI.Instance.SoundWinResult);
                Time.timeScale = 0f;
                break;
            case ResultType.Lose:
                _resultScreenObject.SetActive(true);
                _resultText.text = _loseResultInscription;
                SoundMakerGUI.Instance.PlaySound(SoundMakerGUI.Instance.SoundLoseResult);
                Time.timeScale = 0f;
                break;

        }
    }

    public void SetLevelData(int RealmIndex, int LevelIndex)
    {
        _realmIndex = RealmIndex;
        _levelIndex = LevelIndex;
    }

    private void SaveResult(bool result = true)
    {
        //получаем сэйвДату из файлика
        SavedData data = _saveService.LoadData();

        //вносим изменения по индексу
        if(_realmIndex != 0 && _levelIndex != 0)
            data.RealmsData[_realmIndex - 1].LevelsData[_levelIndex - 1] = result;
        else
            data.RealmsData[_realmIndex].LevelsData[_levelIndex] = result;

        //сохраняем измекненую сэйвДату
        _saveService.SaveData(data);
    }

}

public enum ResultType
{
    Win,
    Lose
}