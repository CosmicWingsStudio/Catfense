
using UnityEngine;
using Zenject;

public class EnvironmentHandler : MonoBehaviour
{
    [Inject] PrefabsPathsToFoldersProvider _prefabsData;

    public void SetEnvironment(string PrefabName)
    {    
        Instantiate(Resources.Load(_prefabsData.EnvironmentPrefabsPath + PrefabName), transform);  
    }
}
