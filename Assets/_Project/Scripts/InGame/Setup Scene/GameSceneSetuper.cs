using UnityEngine;
using Zenject;

public class GameSceneSetuper : IInitializable
{
    private BackGroundHandler _backGroundHandler;
    private EnvironmentHandler _environmentHandler;
    private SceneEnemyFabric _fabricPlaner;

    public GameSceneSetuper(BackGroundHandler bgh, EnvironmentHandler eh, SceneEnemyFabric fp)
    {
        _backGroundHandler = bgh;
        _environmentHandler = eh;
        _fabricPlaner = fp;
    }

    private LevelConfig CurrentLevelDataConfig;

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
            CurrentLevelDataConfig = LevelDataProviderFromMenuScene.Instance.LevelDataConfig;
        }
        else
            Debug.LogError("LevelDataProvider is missing on the scene");
        
    }

    private void SetParamsFromLevelConfig()
    {
        if (CurrentLevelDataConfig != null )
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
        if(CurrentLevelDataConfig.BackGround == null)
        {
            Debug.LogError("Background image is not set");
            Application.Quit();
        }
        else
        {
            try
            {
                _backGroundHandler.SetBackGround(CurrentLevelDataConfig.BackGround);
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
        if (CurrentLevelDataConfig.BackGround == null)
        {
            Debug.LogError("Environment Prefab Name is not set");
            Application.Quit();
        }
        else
        {
            try
            {
                _environmentHandler.SetEnvironment(CurrentLevelDataConfig.EnvironmentPrefabName);
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
        Debug.Log("FABRIC PLAN IS ADDED");
    }
  
}
