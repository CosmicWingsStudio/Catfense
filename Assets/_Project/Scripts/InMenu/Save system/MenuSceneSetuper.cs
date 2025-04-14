using System.Collections;
using UnityEngine;
using YG;
using Zenject;

public class MenuSceneSetuper : MonoBehaviour
{
    [Inject] readonly ISaveService saveService;
    [Inject] readonly RealmsHandler realmsHandler;

    private bool IsSetUp = false;

    private void Start()
    {
        if (YandexGame.SDKEnabled)
        {
            SetUpScene();
        }
    }

    private void SetUpScene()
    {
        if (!IsSetUp)
        {
            IsSetUp = true;
            saveService.SetData(saveService.LoadData());
            realmsHandler.Initialize();
            Debug.Log("scene is set up");
            StartCoroutine(GameReadyInitDelay());
        }
        
    }

    private IEnumerator GameReadyInitDelay()
    {
        yield return new WaitForSeconds(1.75f);
        if(LevelDataProviderFromMenuScene.Instance.IsGRAReady == false)
        {
            YandexGame.GameReadyAPI();
            LevelDataProviderFromMenuScene.Instance.IsGRAReady = true;
            Debug.Log("GRA IS READU");

        }
        //YandexGame.GameplayStart();
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += SetUpScene;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= SetUpScene;
    }
}
