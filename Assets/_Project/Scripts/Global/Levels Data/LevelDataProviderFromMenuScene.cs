using UnityEngine;

public class LevelDataProviderFromMenuScene : MonoBehaviour
{
    public static LevelDataProviderFromMenuScene Instance;
 
    public LevelConfig LevelDataConfig { get; set; }

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
