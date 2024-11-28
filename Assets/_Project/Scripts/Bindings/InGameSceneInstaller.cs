using UnityEngine;
using Zenject;

public class InGameSceneInstaller : MonoInstaller
{
    [SerializeField] private SpriteRenderer _backGroundRenderer;
    [SerializeField] private PrefabsPathsToFoldersProvider _prefabsDataConfig;

    public override void InstallBindings()
    {
        BindSignals();
        BindConfigs();
        BindSceneSetupServices();
        //BindFactories();
        BindHandlers();
        //BindModules();
        //BindDeveloperTools();
    }

    private void BindSignals()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<PausedSignal>();
        Container.DeclareSignal<UnpausedSignal>();
        Container.DeclareSignal<WaveStartedSignal>();
        Container.DeclareSignal<WaveEndedSignal>();
        Container.DeclareSignal<LevelEndedSignal>();
    }

    private void BindConfigs()
    {
        Container.Bind<PrefabsPathsToFoldersProvider>().FromInstance(_prefabsDataConfig).AsSingle();
    }

    private void BindSceneSetupServices()
    {
        BackGroundHandler bgHandler = new(_backGroundRenderer);

        Container.Bind<BackGroundHandler>().FromInstance(bgHandler).AsSingle();
        Container.Bind<EnvironmentHandler>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SceneEnemyFactory>().FromComponentInHierarchy().AsSingle();

        Container.BindInterfacesTo<GameSceneSetuper>().AsSingle();   
    }

    private void BindHandlers()
    {
        Container.Bind<GameModeSwitcher>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PreparationPhaseGUIHandler>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ResultScreenGUIHandler>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PauseGUIHandler>().FromComponentInHierarchy().AsSingle();

    }

    //private void BindDeveloperTools()
    //{
    //    Debug.LogWarning("YOU ARE IN DEV MODE. DO NOT FORGET TO DISABLE _DEVTOOLS BEFORE REALESE");
    //    try
    //    {
    //        Container.Bind<DeveloperHandler>().FromComponentInHierarchy().AsSingle();
    //    }
    //    catch (System.Exception)
    //    {
    //        Debug.LogError("DEV TOOLS AVALIEBLE ONLY WHEN STARTS FROM GAME SCENE, NOT FROM MENU");
    //    }
        
    //}

    //private void BindModules()
    //{
        
    //}

    //private void BindFactories()
    //{
    //    Container.BindFactory<Enemy, EnemyFactory>();
    //}
}
