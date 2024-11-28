using UnityEngine;


public class DeveloperHandler : MonoBehaviour
{
    [Header("References to setable scripts")]
    [SerializeField] private SceneEnemyFactory _enemyFactory;
    [SerializeField] private GameModeSwitcher _gameModeSwitcher;
    //wallet

    [field:Header("Dev game parameters")]
    [field:SerializeField] public bool DisableEnemySpawn { get; private set; }
    [field:SerializeField] public bool EverlastingPreparationTime { get; private set; }
    [field:SerializeField] public int StartMoney { get; private set; }

    private void Start() => ApplyDevSettings();
    
    private void ApplyDevSettings()
    {
        _enemyFactory.IsSpawnDisabledByDevTools = DisableEnemySpawn;
        _gameModeSwitcher.IsEverlastingPreparationTimeByDevTools = EverlastingPreparationTime;
        Debug.LogWarning("YOU ARE IN DEVELOPER MODE");
    }
}
