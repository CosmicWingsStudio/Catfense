using UnityEngine;

[RequireComponent(typeof(RealmLevelFiller))]
public class RealmLevel : MonoBehaviour
{
    public bool IsCompleted = false;
    [ReadOnly] public bool IsAvaliable = false;

    [SerializeField] private GameObject DisableScreen;
    [SerializeField] private UnityEngine.UI.Button PlayButton;

    private LevelConfig levelConfig;

    private void Awake()
    {
        levelConfig = GetComponent<RealmLevelFiller>().GetLevelConfig();

        if (PlayButton != null)
            PlayButton.onClick.AddListener(StartGame);
    }
    public void MakeLevelAvailable()
    {
        IsAvaliable = true;
        DisableScreen.SetActive(false);
    }

    private void StartGame()
    {
        Debug.Log("Sending" + levelConfig + " as a callback");
    }
}
