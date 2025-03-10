using UnityEngine;
using Zenject;

public class GameSceneSetuper : IInitializable
{
    private BackGroundHandler _backGroundHandler;
    private EnvironmentHandler _environmentHandler;
    private SceneEnemyFactory _sceneEnemyFactory;
    private MusicController _musicController;

    public GameSceneSetuper(BackGroundHandler bgh, EnvironmentHandler eh,
        SceneEnemyFactory fp, MusicController mc)
    {
        _backGroundHandler = bgh;
        _environmentHandler = eh;
        _sceneEnemyFactory = fp;
        _musicController = mc;
    }

    private LevelConfig LevelDataConfig;

    public void Initialize() => SetupScene();
    
    private void SetupScene()
    {
        Debug.Log("Setup scene process starts");
        TakeLevelConfigData();
        SetParamsFromLevelConfig();
        if (LevelDataProviderFromMenuScene.Instance.OnRestart)
        {
            LevelDataProviderFromMenuScene.Instance.OnRestart = false;
        }
        else
        {
            LevelDataProviderFromMenuScene.Instance.RestartLevelDataSaverSO.SetDefaultData();
        }
    }

    private void TakeLevelConfigData()
    {
        if (LevelDataProviderFromMenuScene.Instance != null)
        {
            if(LevelDataProviderFromMenuScene.Instance.LevelDataConfig != null)
                LevelDataConfig = LevelDataProviderFromMenuScene.Instance.LevelDataConfig;
            else if(LevelDataProviderFromMenuScene.Instance.DeveloperToolsConfig != null)
                LevelDataConfig = LevelDataProviderFromMenuScene.Instance.DeveloperToolsConfig;
        }
        else
            Debug.LogError("LevelDataProvider is missing on the scene");
        
    }

    private void SetParamsFromLevelConfig()
    {
        if (LevelDataConfig != null )
        {
            SetBackGroundImage();
            SetEnvironment();
            FillFactoryWithData();
            SetAudioClips();
        }  
        else
            Debug.LogError("LevelConfig is missing");
    }

    private void SetBackGroundImage()
    {
        if(LevelDataConfig.BackGround == null)
        {
            Debug.LogError("Background image is not set");
            Application.Quit();
        }
        else
        {
            try
            {
                _backGroundHandler.SetBackGround(LevelDataConfig.BackGround);
                Debug.Log("CONCRETE BG HAS BEEN INSTALLED");
            }
            catch (System.Exception)
            {
                Debug.LogError("Can not set the background image");
                Application.Quit();
            }
           
        }
        
    }

    private void SetEnvironment()
    {
        if (LevelDataConfig.BackGround == null)
        {
            Debug.LogError("Environment Prefab Name is not set");
            Application.Quit();
        }
        else
        {
            try
            {
                _environmentHandler.SetEnvironment(LevelDataConfig.EnvironmentPrefabName);
                Debug.Log("CONCRETE ENV HAS BEEN INSTALLED");
            }
            catch (System.Exception)
            {
                Debug.LogError("Environment Prefab Name is invalid");
                Application.Quit();
            }

        }      
    }

    private void SetAudioClips()
    {
        if (LevelDataConfig.BackGround == null)
        {
            Debug.LogError("Audio Clips failed");
            Application.Quit();
        }
        else
        {
            try
            {
                _musicController.SetData(LevelDataConfig.MusicClips);
            }
            catch (System.Exception)
            {
                Application.Quit();
            }

        }
    }

    private void FillFactoryWithData()
    {
        if(LevelDataConfig.WavesList.Count == 0)
        {
            Debug.LogError("EnemyWaves is not set");
            Application.Quit();
        }
        else
        {     

            Transform enemySpawnPoint = _environmentHandler.GetEnemySpawnPoint();
            _sceneEnemyFactory.SetConfigData(LevelDataConfig.WavesList, LevelDataConfig.WavesAmount,
                enemySpawnPoint, LevelDataConfig.DifficultyLevel);
            Debug.Log("WavesCfg is installed");
        }
     
    }
  
}
