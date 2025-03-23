using UnityEngine;

public class DisableImageHandler : MonoBehaviour
{
    [SerializeField] private GameObject _screen;

    private void OnEnable()
    {
        _screen.SetActive(true);
    }

    private void OnDisable()
    {
        _screen.SetActive(false);
    }
}
