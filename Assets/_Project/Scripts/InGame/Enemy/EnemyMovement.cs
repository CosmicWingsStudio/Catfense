
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool CanMove { get; set; } = true;

    [SerializeField] private GameObject _slownessIcon;

    private bool IsSlowed = false;
    private float _slownessEffectValue = 0f;
    private float _defaultSlownessDecayTime = 5f;
    private float _slownessDecayTimer = 0f;

    private void Update()
    {
        if (!CanMove)
            return;

        if (IsSlowed)
        {
            if (_slownessDecayTimer < _defaultSlownessDecayTime)
                _slownessDecayTimer += Time.deltaTime;
            else
            {
                IsSlowed = false;
                _slownessEffectValue = 0f;
                _slownessIcon.SetActive(false);
            }
        }
        
        
        Vector2 vec2 = transform.position;
        float defaultSpeed = 0.6f;
        float speed = defaultSpeed - _slownessEffectValue;
        vec2.x += -Time.deltaTime * speed;
        transform.position = vec2;
    }

    public void SetSlownessEffect(float coeffOfSlowness)
    {
        IsSlowed = true;
        _slownessDecayTimer = 0f;
        _slownessEffectValue = coeffOfSlowness;
        _slownessIcon.SetActive(true);
    }

}
