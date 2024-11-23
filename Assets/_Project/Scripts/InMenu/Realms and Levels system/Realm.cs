using UnityEngine;
using UnityEngine.UI;

public class Realm : MonoBehaviour
{
    public bool IsCompleted = false;
    [ReadOnly] public bool IsAvaliable = false;

    [SerializeField] private Button OpenRealmButton;

    private void Awake()
    {
        OpenRealmButton.enabled = false;
    }
    public void MakeRealmAvailable()
    {
        IsAvaliable = true;
        OpenRealmButton.enabled = true;
    }
}
