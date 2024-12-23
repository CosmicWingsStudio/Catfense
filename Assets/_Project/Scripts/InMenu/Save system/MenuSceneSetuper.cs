using UnityEngine;
using Zenject;

public class MenuSceneSetuper : MonoBehaviour
{
    [Inject] readonly ISaveService saveService;

    private void Start()
    {
        saveService.SetData(saveService.LoadData());
        //Debug.Log(saveService.GetSaveDataPath());
    }
}
