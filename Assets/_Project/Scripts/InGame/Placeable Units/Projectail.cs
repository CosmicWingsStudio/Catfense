using TMPro;
using UnityEngine;

public class Projectail : MonoBehaviour
{
    private float _damage;
    private float _projectailSpeed;
    private Transform _target;
    private Vector3 _lastPosition;
    private bool IsIntialized = false;
    private bool hit = false;
    private bool OnNullTarget = false;

    public void Initialize(float damage, float projectailSpeed, Transform target)
    {
        _damage = damage;
        _target = target;
        _projectailSpeed = projectailSpeed;
        _lastPosition = target.position;

        IsIntialized = true;
    }

    private void Update()
    {
        if (hit)
            return;

        if(IsIntialized)
        {
            if(_target == null && OnNullTarget == false)
            {
                OnNullTarget = true;
                return;
            }

            if (OnNullTarget)
            {
                transform.position = Vector2.MoveTowards(transform.position, _lastPosition, _projectailSpeed * Time.deltaTime);

                if ((int)transform.position.y * 100 == (int)_lastPosition.y * 100 && (int)transform.position.x * 100 == (int)_lastPosition.x * 100)
                {
                    Destroy(gameObject);
                }

                return;
            }

            transform.position = Vector2.MoveTowards(transform.position, _target.position, _projectailSpeed * Time.deltaTime);
            _lastPosition = _target.position;
        }
        
        if ((int)transform.position.y * 100 == (int)_target.position.y * 100 && (int)transform.position.x * 100 == (int) _target.position.x * 100)
        {
            _target.GetComponent<HealthHandler>().TakeDamage(_damage);
            hit = true;
            Destroy(gameObject);
        }
    }
}
