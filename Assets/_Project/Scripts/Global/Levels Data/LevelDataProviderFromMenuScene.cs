using UnityEngine;

public class LevelDataProviderFromMenuScene : MonoBehaviour
{
    public static LevelDataProviderFromMenuScene Instance;

    [field:SerializeField, Tooltip("Allows not change BG, ENV, FABRICPLAN to default settings")]
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
