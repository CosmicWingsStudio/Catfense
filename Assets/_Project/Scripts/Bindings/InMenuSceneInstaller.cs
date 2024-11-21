using UnityEngine;
using Zenject;

public class InMenuSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindSignals();
    }

    private void BindSignals()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<PausedSignal>();
        Container.DeclareSignal<UnpausedSignal>();
    }
}
