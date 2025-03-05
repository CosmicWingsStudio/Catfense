using System.Collections;
using UnityEngine;

public class SpellObject : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private float _damage;
    [SerializeField] private GameObject _fxObject;

    private Transform _enemyFolder;
    private bool IsInitialised = false;

    public void Initialize(Transform enemyFolder, AudioSource asource)
    {
        if(IsInitialised == false)
        {
            StartCoroutine(SpellEffect(asource));
            _enemyFolder = enemyFolder;
            IsInitialised = true;
        }       
    }
    private IEnumerator SpellEffect(AudioSource asource)
    {
        yield return new WaitForSeconds(_delay);
        _fxObject.SetActive(true);
        asource.Play();
        yield return new WaitForSeconds(_delay * 0.5f);
        for (int i = 0; i < _enemyFolder.childCount; i++)
        {
            if (_enemyFolder.GetChild(i).TryGetComponent(out EnemyUnit eUnit))
            {
                eUnit.unitHealth.TakeDamage(_damage);
            }
        }
    }
}
