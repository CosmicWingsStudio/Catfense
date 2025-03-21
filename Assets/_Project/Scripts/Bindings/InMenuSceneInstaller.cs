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
        DesktopSaveService desktopSaveService = new(_realmsDataHandler);
        Debug.Log("DESKTOP SERVICE INITILISED");
        //Container.Bind<ISaveService>().To<DesktopSaveService>().FromInstance(desktopSaveService).AsSingle();
        Container.BindInterfacesAndSelfTo<DesktopSaveService>().FromInstance(desktopSaveService).AsSingle();
    }

}
