using UnityEngine;
using Zenject;

public class InGameSceneInstaller : MonoInstaller
{
    [SerializeField] private SpriteRenderer _backGroundRenderer;
    [SerializeField] private PrefabsData _prefabsDataConfig;

    public override void InstallBindings()
    {
        BindSignals();
        BindConfigs();
        BindSceneSetupServices();
        //BindFactories();
        BindHandlers();
    }

    private void BindSignals()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<PausedSignal>();
        Container.DeclareSignal<UnpausedSignal>();
        Container.DeclareSignal<WaveStartedSignal>();
        Container.DeclareSignal<WaveEndedSignal>();
    }

    private void BindConfigs()
    {
        Container.Bind<PrefabsData>().FromInstance(_prefabsDataConfig).AsSingle();
    }

    private void BindSceneSetupServices()
    {
        BackGroundHandler bgHandler = new(_backGroundRenderer);

        Container.Bind<BackGroundHandler>().FromInstance(bgHandler).AsSingle();
        Container.Bind<EnvironmentHandler>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SceneEnemyFabric>().FromComponentInHierarchy().AsSingle();

        Container.BindInterfacesTo<GameSceneSetuper>().AsSingle();
    }

    private void BindHandlers()
    {
        Container.Bind<GameModeSwitcher>().FromComponentInHierarchy().AsSingle();
    }

    //private void BindFactories()
    //{
    //    Container.BindFactory<Enemy, EnemyFactory>();
    //}
}
