using UnityEngine;

public class LevelDataProviderFromMenuScene : MonoBehaviour
{
    public static LevelDataProviderFromMenuScene Instance;
    public LevelConfig LevelDataConfig { get; set; }

    [field:Header("Dev testing field")]
    [field: SerializeField, Tooltip("Use this only for testing and on GameScene")]
    public LevelConfig DeveloperToolsConfig { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
}
