using System.Collections;
using TMPro;
using UnityEngine;

public class HealingText : MonoBehaviour
{
    [SerializeField] private Transform _endPosition;
    [SerializeField] private Transform _startPosition;
    private TextMeshProUGUI _text;
    private float _floatingTime = 1.1f;
    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(false); 
    }

    public void OnEnableCustom(float healAmount)
    {
        StopCoroutine(FloatingAnimation());
        transform.position = RandomizeStartPosition();
        Color color = _text.color;
        color.a = 255f;
        _text.color = color;
        _text.text = "+" + healAmount.ToString();
        StartCoroutine(FloatingAnimation());
    }

    private IEnumerator FloatingAnimation()
    {
        float timer = 0f;
        float step = 1f / _floatingTime;
        Vector2 newPos = new(transform.position.x, _endPosition.position.y);
        Color newColor = _text.color;
        newColor.a = 0f;
        while (timer < _floatingTime)
        {
            timer += Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, newPos, step * timer);
            _text.color = Color.Lerp(_text.color, newColor, step * timer);
            yield return null;
        }

        gameObject.SetActive(false);
        yield return null;
    }

    private Vector3 RandomizeStartPosition()
    {
        Vector3 newPos = _startPosition.position;
        newPos.x += Random.Range(-1.25f, 1.25f);
        return newPos;
    }
}
