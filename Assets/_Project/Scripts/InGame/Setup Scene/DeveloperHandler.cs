using UnityEngine;


public class DeveloperHandler : MonoBehaviour
{
    [Header("References to setable scripts")]
    [SerializeField] private SceneEnemyFactory _enemyFactory;
    [SerializeField] private GameModeSwitcher _gameModeSwitcher;
    [SerializeField] private WalletHandler _walletHandler;

    [field:Header("Dev game parameters")]
    [field:SerializeField] public bool DisableEnemySpawn { get; private set; }
    [field:SerializeField] public bool EverlastingPreparationTime { get; private set; }
    [field:SerializeField] public int StartMoney { get; private set; }

    private bool Initialized = false;
    private void LateUpdate()
    {
        if(Initialized == false)
        {
            ApplyDevSettings();
            Initialized = true;
        }
    }

    private void ApplyDevSettings()
    {
        _enemyFactory.IsSpawnDisabledByDevTools = DisableEnemySpawn;
        _gameModeSwitcher.IsEverlastingPreparationTimeByDevTools = EverlastingPreparationTime;
        _walletHandler.AddMoney(StartMoney);
        Debug.LogWarning("YOU ARE IN DEVELOPER MODE");
    }
}
