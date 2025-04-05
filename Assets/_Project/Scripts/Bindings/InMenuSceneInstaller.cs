using UnityEngine;
using Zenject;

public class InMenuSceneInstaller : MonoInstaller
{
    [SerializeField] private RealmsDataHandler _realmsDataHandler;

    public override void InstallBindings()
    {
        BindSignals();
        BindHandlers();
        BindSaveServices();
    }

    private void BindSignals()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<PausedSignal>();
        Container.DeclareSignal<UnpausedSignal>();
        Container.DeclareSignal<ADVideoStartedSignal>();
        Container.DeclareSignal<ADVideoEndedSignal>();
    }

    private void BindHandlers()
    {
        Container.Bind<RealmsDataHandler>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MenuSceneSetuper>().FromComponentInHierarchy().AsSingle();
        Container.Bind<RealmsHandler>().FromComponentInHierarchy().AsSingle();
    }

    private void BindSaveServices()
    {
        YandexGamesSaveService ygSaveService = new(_realmsDataHandler);
        Container.BindInterfacesAndSelfTo<YandexGamesSaveService>().FromInstance(ygSaveService).AsSingle();
    }

}
