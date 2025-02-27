
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RewardObject : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TMPro.TextMeshProUGUI _moneyValueText;
    
    private WalletHandler _walletHandler;
    private Vector2 _moneyTextPosition;
    private Image _image;
    private Animator _animator;
    private bool IsInititalised = false;
    private bool IsTaken = false;
    private bool OnDestroy = false;
    private float _moneyValue;

    [SerializeField] private float _fadeAwayTime = 1.25f;
    private float _fadeAwayTimer = 0f;

    public void Initialize(WalletHandler wallet, Vector2 moneyTextPos, float moneyValue)
    {
        if (!IsInititalised)
        {
            _walletHandler = wallet;
            _moneyTextPosition = moneyTextPos;
            _moneyValue = moneyValue;
            _moneyValueText.text = _moneyValue.ToString();
            _image = GetComponent<Image>();
            _animator = GetComponent<Animator>();
            IsInititalised = true;
        }
    }

    private void Update()
    {
        if (OnDestroy)
            return;

        if(IsTaken == false)
        {
            if (_fadeAwayTimer >= _fadeAwayTime)
            {
                Destroy(gameObject);
            }
            else
                _fadeAwayTimer += Time.deltaTime;
        }
        else
        {
            Vector2 tposv2 = (Vector2)transform.position;
            Color newColor = _image.color;
            newColor.a -= Time.deltaTime;
            _image.color = newColor;
            _moneyValueText.alpha -= Time.deltaTime;
            if (tposv2.magnitude + 1 >= _moneyTextPosition.magnitude)
            {
                _walletHandler.AddMoney((int)_moneyValue);
                Destroy(gameObject);
                OnDestroy = true;
            }
            else
                transform.position = Vector3.Lerp(transform.position, _moneyTextPosition, 5 * Time.deltaTime);
        }
    }

    public void ChangeColor(float value)
    {
        switch (value)
        {
            case 0:
                _image.color = Color.red;
                break;
            case 1:
                _image.color = Color.blue;
                break;
            case 2:
                _image.color = Color.green;
                break;
            case 3:
                _image.color = Color.yellow;
                break;
            case 4:
                _image.color = Color.cyan;
                break;
            default:
                _image.color = Color.white;
                break;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsTaken = true;
        _animator.enabled = false;
    }
}
