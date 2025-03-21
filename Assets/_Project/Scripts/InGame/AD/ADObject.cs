using System.Collections;
using UnityEngine;
using Zenject;

public class ADObject : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 1;

    private SignalBus _signalBus;
    private bool IsInitialised = false;

    public void Initialize(SignalBus sb)
    {
        if(IsInitialised == false)
        {
            IsInitialised = true;
            _signalBus = sb;
            StartCoroutine(DestroyDelay());
        }    
    }

    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(_lifeTime);
        _signalBus.Fire<ADVideoEndedSignal>();
        Destroy(gameObject);    
    }
}
