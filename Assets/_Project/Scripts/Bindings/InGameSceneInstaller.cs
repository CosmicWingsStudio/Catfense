using UnityEngine;
using Zenject;

public class InGameSceneInstaller : MonoInstaller
{
    [SerializeField] private UnityEngine.UI.Image _backGroundImage;
    [SerializeField] private PrefabsPathsToFoldersProvider _prefabsDataConfig;

    public override void InstallBindings()
    {
        BindSignals();
        BindConfigs();
        BindSceneSetupServices();
        BindFactories();
        BindHandlers();
        BindSaveServices();
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
        BackGroundHandler bgHandler = new(_backGroundImage);

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
        Container.Bind<WalletHandler>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SellUnitGUIHandler>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ShopHandler>().FromComponentInHierarchy().AsSingle();
        Container.Bind<UnitDragPlacer>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GUIWarningHandler>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MusicController>().FromComponentInHierarchy().AsSingle();
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

    private void BindSaveServices()
    {
        DesktopSaveService desktopSaveService = new (null);
        //Container.Bind<ISaveService>().To<DesktopSaveService>().FromInstance(desktopSaveService).AsSingle();
        Container.BindInterfacesAndSelfTo<DesktopSaveService>().FromInstance(desktopSaveService).AsSingle();
    }

    private void BindFactories()
    {
        Container.Bind<PlaceableUnitsFactory>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CardsFactory>().FromComponentInHierarchy().AsSingle();
    }
}
