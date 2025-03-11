using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrainingLevel : MonoBehaviour
{
    [SerializeField] private GameObject _trainingPanel;
    [SerializeField] private LevelConfig _trainingLevelConfig;
    [SerializeField] private Button _openTrainingPanel;
    [SerializeField] private Button _startTrainingLevel;

    private void Start()
    {
        _openTrainingPanel.onClick.AddListener(OpenTrainingPanel);
        _startTrainingLevel.onClick.AddListener(StartTrainingLevel);
    }

    private void OpenTrainingPanel()
    {
        _trainingPanel.SetActive(true);
    }

    private void StartTrainingLevel()
    {
        LevelDataProviderFromMenuScene.Instance.LevelDataConfig = _trainingLevelConfig;
        SceneManager.LoadScene("InGameTrainingScene");
    }
}
