using UnityEngine;
using Zenject;

public class GameSceneSetuper : IInitializable
{
    private BackGroundHandler _backGroundHandler;
    private EnvironmentHandler _environmentHandler;
    private SceneEnemyFactory _sceneEnemyFactory;

    public GameSceneSetuper(BackGroundHandler bgh, EnvironmentHandler eh, SceneEnemyFactory fp)
    {
        _backGroundHandler = bgh;
        _environmentHandler = eh;
        _sceneEnemyFactory = fp;
    }

    private LevelConfig LevelDataConfig;

    public void Initialize() => SetupScene();
    
    private void SetupScene()
    {
        Debug.Log("Setup scene process starts");
        TakeLevelConfigData();
        SetParamsFromLevelConfig();
    }

    private void TakeLevelConfigData()
    {
        if (LevelDataProviderFromMenuScene.Instance != null)
        {
            LevelDataConfig = LevelDataProviderFromMenuScene.Instance.LevelDataConfig;
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
            SetFabricPlan();
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

    private void SetFabricPlan()
    {
        if(LevelDataConfig.WavesList.Count == 0)
        {
            Debug.LogError("EnemyWaves is not set");
            Application.Quit();
        }
        else
        {
            try
            {
                _sceneEnemyFactory.SetConfigData(LevelDataConfig.WavesList, LevelDataConfig.WavesAmount);
                Debug.Log("WavesCfg is installed");
            }
            catch (System.Exception)
            {
                Debug.LogError("Can't set config data to SceneEnemyFactory");
                Application.Quit();
            }
        }
     
    }
  
}
