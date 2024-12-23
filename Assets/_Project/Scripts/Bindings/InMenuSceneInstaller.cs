using UnityEngine;
using Zenject;

public class InMenuSceneInstaller : MonoInstaller
{
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
    }

    private void BindHandlers()
    {
        Container.Bind<RealmsDataHandler>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MenuSceneSetuper>().FromComponentInHierarchy().AsSingle();
    }

    private void BindSaveServices()
    {
        Container.BindInterfacesAndSelfTo<DesktopSaveService>().AsSingle();
    }

}
