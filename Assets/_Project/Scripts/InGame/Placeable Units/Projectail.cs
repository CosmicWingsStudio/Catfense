
using UnityEngine;
public class Projectail : MonoBehaviour
{
    [SerializeField] private bool NoRotation = false;
    [SerializeField] private bool AOEDamage = false;
    [SerializeField, Min(0)] private float AOEDamageX = 1f;
    [SerializeField, Min(0)] private float AOEDamageY = 1f;
    [SerializeField, Tooltip("Can be null")] private GameObject OnHitEffect;
    private float _slownessOnHit;
    private float _damage;
    private float _projectailSpeed;
    private Transform _targetObject;
    private Transform _targetPosition;
    private Vector3 _lastPosition;
    private bool IsInitialized = false;
    private bool hit = false;
    private bool OnNullTarget = false;

    public void Initialize(float damage, float projectailSpeed, Transform target, float additionalAOE, float slownessEffect = 0f)
    {
        if(IsInitialized)
            return;

        _damage = damage;
        _slownessOnHit = slownessEffect;
        _targetObject = target;
        if(additionalAOE > 0 && AOEDamage)
        {
            AOEDamageX += AOEDamageX * additionalAOE;
            AOEDamageY += AOEDamageX * additionalAOE;
        }

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
                            if (OnHitEffect != null)
                            {
                                GameObject onHitEffect = Instantiate(OnHitEffect);
                                onHitEffect.transform.position = transform.position;
                            }
                            if (_slownessOnHit > 0)
                            {
                                eu.GetComponent<EnemyMovement>().SetSlownessEffect(_slownessOnHit);
                            }

                            hit = true;

                            if (AOEDamage == false)
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
                if (AOEDamage)
                {
                    var colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(AOEDamageY, AOEDamageX), 90);
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        if (colliders[i].transform.TryGetComponent(out EnemyUnit eu))
                        {
                            eu.GetComponent<HealthHandler>().TakeDamage(_damage);
                            if (OnHitEffect != null)
                            {
                                GameObject onHitEffect = Instantiate(OnHitEffect);
                                onHitEffect.transform.position = transform.position;
                            }
                            if(_slownessOnHit > 0)
                            {
                                eu.GetComponent<EnemyMovement>().SetSlownessEffect(_slownessOnHit);
                            }

                            hit = true;  
                        }
                    }
                }
                else
                {
                    _targetObject.GetComponent<HealthHandler>().TakeDamage(_damage);
                    if (_slownessOnHit > 0)
                    {
                        _targetObject.GetComponent<EnemyMovement>().SetSlownessEffect(_slownessOnHit);
                    }

                    hit = true;

                    if (OnHitEffect != null)
                    {
                        GameObject onHitEffect = Instantiate(OnHitEffect);
                        onHitEffect.transform.position = new Vector2(
                            Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f),
                            Random.Range(transform.position.y - 0.5f, transform.position.y + 0.5f));
                    }
                }

                Destroy(gameObject);
            }

           
        }
                
    }

    private void OnValidate()
    {
        if(AOEDamage == false)
        {
            AOEDamageX = 1;
            AOEDamageY = 1;
        }
    }
}
