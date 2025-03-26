using UnityEngine;
using Zenject;

public class MenuSceneSetuper : MonoBehaviour
{
    [Inject] readonly ISaveService saveService;
    [Inject] readonly RealmsHandler realmsHandler;

    private void Start()
    {
        Application.targetFrameRate = 100;
        saveService.SetData(saveService.LoadData());
        realmsHandler.Initialize();
        //Debug.Log(saveService.GetSaveDataPath());
    }
}
