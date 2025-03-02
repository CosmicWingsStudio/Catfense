using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UltimateIcon : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private UnitUltimate _unitUltmate;
    [SerializeField] private float _livingTime;

    private Image _icon;
    private Color _defaultColor;

    private float _timer = 0f;
    private bool IsActive = false;
    private bool IsClicked = false;
    private bool IsInitialized = false;

    private void Start()
    {
        _icon = GetComponent<Image>();
        _defaultColor = _icon.color;
        Color newColor = _defaultColor;
        newColor.a = 0f;
        _icon.color = newColor;
        IsInitialized = true;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!IsActive && IsClicked)
            return;

        if (_timer < _livingTime)
            _timer += Time.deltaTime;
        else
            StartCoroutine(IconDisappears());
    }

    private void OnEnable()
    {
        if(!IsInitialized) return;

        _timer = 0f;
        IsClicked = false;
        StartCoroutine(IconAppears());
    }

    private IEnumerator IconAppears()
    { 
        float timer = 0f;
        float step = 1f / 0.3f;
        while (timer < 0.3f)
        {
            timer += Time.deltaTime;
            _icon.color = Color.Lerp(_icon.color, _defaultColor, step * timer);

            if (timer >= 0.3f)
            {
                IsActive = true;
                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator IconDisappears(bool disappearFaster = false)
    {
        float timer = 0f;
        float disappearTime = 0.3f;

        if (disappearFaster)
            disappearTime = 0.1f;

        float step = 1f / disappearTime;
        while (timer < disappearTime)
        {
            timer += Time.deltaTime;

            Color color = _icon.color;
            color.a = Mathf.Lerp(_icon.color.a, 0f, step * timer);
            _icon.color = color;

            if (timer >= disappearTime)
            {
                IsActive = false;
                gameObject.SetActive(false);
                yield break;
            }

            yield return null;
        }

    }

    private IEnumerator IconBlink()
    {
        float timer = 0f;
        float step = 1f / 0.25f;
        _icon.color = Color.black;
        while(timer < 0.25f)
        {
            timer += Time.deltaTime;
            _icon.color = Color.Lerp(_icon.color, _defaultColor, step * timer);

            if (timer >= 0.25f)
            {
                StartCoroutine(IconDisappears(true));
                yield break;
            }

            yield return null;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsActive)
        {
            IsClicked = true;
            Action();
            StartCoroutine(IconBlink());
        }
    }

    private void Action()
    {
        _unitUltmate.DoUlitmateAction();
    }
   
}
