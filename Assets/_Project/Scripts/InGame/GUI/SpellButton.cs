using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellButton : MonoBehaviour
{
    [SerializeField] private GameObject _spellPrefab;
    [SerializeField] private Transform _castPosition;
    [SerializeField, Min(1f)] private float _spellCooldown; 

    private TextMeshProUGUI _cooldownText;
    private Button _button;
    private float _cooldownTimer = 0f;
    private bool IsReady = false;

    private void Start()
    {
        _cooldownText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _cooldownText.text = string.Empty;
        _button = GetComponent<Button>();

        _button.onClick.AddListener(SpellAction);
    }

    private void Update()
    {
        if (IsReady)
            return;

        if(_cooldownTimer < _spellCooldown)
        {
            _cooldownTimer += Time.deltaTime;
            int timer = (int)_cooldownTimer;
            _cooldownText.text = timer.ToString();
        }
        else
        {
            IsReady = true;
            _button.interactable = true;
            _cooldownTimer = 0F;
        }
    }

    private void SpellAction()
    {
        IsReady = false;
        _button.interactable = false;
        GameObject spellObject = Instantiate(_spellPrefab);
        spellObject.transform.position = _castPosition.position; 
    }
}
