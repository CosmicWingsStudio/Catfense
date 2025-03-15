using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class ResultScreenGUIHandler : MonoBehaviour
{
    private SignalBus _signalBus;
    private ISaveService _saveService;
    private LevelConfig _levelConfig;
    private int _realmIndex;
    private int _levelIndex;
    private int _additionalMoney = 0;

    [Header("Put references")]
    [SerializeField] private RestartLevelDataSaver _restartLevelDataSaverSO;
    [SerializeField] private GameObject _resultScreenObject;
    [SerializeField] private GameObject _additionalOnLose;
    [SerializeField] private TextMeshProUGUI _resultText;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _restartButton;

    [Header("Put string values")]

    [SerializeField] private string _winResultInscription = "Уровень пройден";
    [SerializeField] private string _loseResultInscription = "Уровень не пройден";
    [SerializeField] private bool IsTrainingLevel = false;

    [Inject]
    private void Initialize(SignalBus signalBus, ISaveService saveService)
    {
        _signalBus = signalBus;
        _saveService = saveService;

        _signalBus.Subscribe<LevelEndedSignal>(ShowResultScreen);
        _signalBus.Subscribe<PRVideoEndedSignal>(() => _additionalMoney = 100);
        _exitButton.onClick.AddListener(() => SceneManager.LoadScene("InMenuScene"));
        _restartButton.onClick.AddListener(RestartLevel);
    }
    
    public void ShowResultScreen(LevelEndedSignal levelEndedSignal)
    {
        if (IsTrainingLevel)
        {
            SoundMakerGUI.Instance.PlaySound(SoundMakerGUI.Instance.SoundWinResult);
            return;
        }

        switch (levelEndedSignal.ResultType)
        {
            case ResultType.Win:
                try
                {
                    SaveResult();
                }
                catch (System.Exception)
                {
                    Debug.LogError("SAVE RESULT IS FAILED");
                    throw;
                }
                _resultScreenObject.SetActive(true);
                _resultText.text = _winResultInscription;
                SoundMakerGUI.Instance.PlaySound(SoundMakerGUI.Instance.SoundWinResult);
                Time.timeScale = 0f;
                break;
            case ResultType.Lose:
                _resultScreenObject.SetActive(true);
                _additionalOnLose.SetActive(true);
                //StartCoroutine(RestartButtonEnableDelay());
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

    private void RestartLevel()
    {
        if (LevelDataProviderFromMenuScene.Instance.LevelDataConfig != null)
            _levelConfig = LevelDataProviderFromMenuScene.Instance.LevelDataConfig;
        else if (LevelDataProviderFromMenuScene.Instance.DeveloperToolsConfig != null)
            _levelConfig = LevelDataProviderFromMenuScene.Instance.DeveloperToolsConfig;

        _restartLevelDataSaverSO.SetData(_additionalMoney, _levelConfig);
        LevelDataProviderFromMenuScene.Instance.Restart();
        Time.timeScale = 1f;
        SceneManager.LoadScene("InGameScene");
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

    private IEnumerator RestartButtonEnableDelay()
    {
        _restartButton.interactable = false;
        Time.timeScale = 1f;
        yield return new WaitForSeconds(1.5f);
        _restartButton.interactable = true;
        Time.timeScale = 0;
    }

}

public enum ResultType
{
    Win,
    Lose
}