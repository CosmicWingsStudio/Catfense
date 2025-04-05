using UnityEngine;
using YG;
using Zenject;

public class MenuSceneSetuper : MonoBehaviour
{
    [Inject] readonly ISaveService saveService;
    [Inject] readonly RealmsHandler realmsHandler;

    

    private void Start()
    {
        Application.targetFrameRate = 100;
        YandexGame.GetDataEvent += SetUpScene;
        if (YandexGame.SDKEnabled)
            SetUpScene();
    }

    private void SetUpScene()
    {
        saveService.SetData(saveService.LoadData());
        realmsHandler.Initialize();
        Debug.Log("scene is set up");
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= SetUpScene;
    }
}
