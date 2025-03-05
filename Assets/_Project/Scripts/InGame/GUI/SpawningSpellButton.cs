using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawningSpellButton : MonoBehaviour
{
    [SerializeField] private SpellObject _spellPrefab;
    [SerializeField] private EnvironmentHandler _envHandler;
    [SerializeField] private Transform _castPosition;
    [SerializeField, Min(1f)] private float _spellCooldown;

    private Transform _enemyFolder;
    private TextMeshProUGUI _cooldownText;
    private Button _button;
    private AudioSource _audioSource;
    private float _cooldownTimer = 0f;
    private bool IsReady = true;

    private void Start()
    {
        _cooldownText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _cooldownText.text = string.Empty;
        _button = GetComponent<Button>();
        _audioSource = GetComponent<AudioSource>();
        _cooldownTimer = _spellCooldown;
        _enemyFolder = _envHandler.GetEnemySpawnPoint();

        _button.onClick.AddListener(SpellAction);
    }

    private void Update()
    {
        if (IsReady)
            return;

        if(_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
            int timer = (int)_cooldownTimer;
            _cooldownText.text = timer.ToString();
        }
        else
        {
            IsReady = true;
            _button.interactable = true;
            _cooldownTimer = _spellCooldown;
            _cooldownText.text = string.Empty; 
        }
    }

    private void SpellAction()
    {
        IsReady = false;
        _button.interactable = false;
        SpellObject spellObject = Instantiate(_spellPrefab);
        spellObject.transform.position = _castPosition.position;
        spellObject.Initialize(_enemyFolder, _audioSource);
    }
}
