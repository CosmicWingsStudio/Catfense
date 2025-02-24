using UnityEngine;

public class DestroyOnTimer : MonoBehaviour
{
    [SerializeField] private float Timer;
    private float _timer;

    private void Update()
    {
        if (_timer >= Timer)
        {
            Destroy(gameObject);
        }
        else
            _timer += Time.deltaTime;
    }
}
