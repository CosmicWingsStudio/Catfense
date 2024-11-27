
using Zenject;
using UnityEngine;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private PrefabsPathsToFoldersProvider _prefabsData;

    public override void InstallBindings()
    {
        BindConfigs();
    }

    private void BindConfigs()
    {
        PrefabsDataProvider prefabsDataProvider = new(_prefabsData);
        Container.Bind<PrefabsDataProvider>().FromInstance(prefabsDataProvider).AsSingle();
    }
}
