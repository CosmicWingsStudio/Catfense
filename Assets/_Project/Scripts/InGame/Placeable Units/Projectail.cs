
using UnityEngine;
public class Projectail : MonoBehaviour
{
    [SerializeField] private bool NoRotation = false;
    private float _damage;
    private float _projectailSpeed;
    private Transform _targetObject;
    private Transform _targetPosition;
    private Vector3 _lastPosition;
    private bool IsInitialized = false;
    private bool hit = false;
    private bool OnNullTarget = false;

    public void Initialize(float damage, float projectailSpeed, Transform target)
    {
        if(IsInitialized)
            return;

        _damage = damage;
        _targetObject = target;

        if (target.childCount > 0)
            _targetPosition = target.GetChild(0);
        else
            _targetPosition = _targetObject;

        _projectailSpeed = projectailSpeed;
        _lastPosition = target.position;

        IsInitialized = true;
    }

    private void Update()
    {
        if (hit)
            return;

        if (IsInitialized)
        {

            if(_targetObject == null && OnNullTarget == false)
            {
                OnNullTarget = true;
                return;
            }

            Vector3 dir;
            Vector3 newDir;

            if (OnNullTarget)
            {
                transform.position = Vector2.MoveTowards(transform.position, _lastPosition, _projectailSpeed * Time.deltaTime);

                if(NoRotation == false)
                {
                    dir = _lastPosition - transform.position;
                    newDir = Vector3.Lerp(transform.right, dir.normalized, Time.deltaTime * 5);
                    transform.right = newDir;
                }

                if ((int)transform.position.y * 100 == (int)_lastPosition.y * 100 && (int)transform.position.x * 100 == (int)_lastPosition.x * 100)
                {
                    var colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(3, 3), 90);
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        if (colliders[i].transform.TryGetComponent(out EnemyUnit eu))
                        {
                            eu.GetComponent<HealthHandler>().TakeDamage(_damage);
                            hit = true;
                            break;
                        }
                    }

                    Destroy(gameObject);
                }

                return;
            }

            transform.position = Vector2.MoveTowards(transform.position, _targetPosition.position, _projectailSpeed * Time.deltaTime);
            _lastPosition = _targetPosition.position;

            if (NoRotation == false)
            {
                dir = _targetPosition.position - transform.position;
                newDir = Vector3.Lerp(transform.right, dir.normalized, Time.deltaTime * 5);
                transform.right = newDir;
            }
      
            if (OnNullTarget) return;

            if ((int)transform.position.y * 100 == (int)_targetPosition.position.y * 100 && (int)transform.position.x * 100 == (int)_targetPosition.position.x * 100)
            {
                _targetObject.GetComponent<HealthHandler>().TakeDamage(_damage);
                hit = true;
                Destroy(gameObject);
            }

           
        }
                
    }
}
