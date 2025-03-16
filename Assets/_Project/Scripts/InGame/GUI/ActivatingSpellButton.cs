
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivatingSpellButton : MonoBehaviour
{

    [SerializeField] private EnvironmentHandler _envHandler;
    [SerializeField, Min(1f)] private float _spellCooldown;
    [SerializeField, Min(1f)] private float _spellTime;

    public float BonusCooldownReduce { get; set; }

    private PlaceSlot _inFrontSlot;
    private TowerHealthHandler _towerHealth;
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
        _inFrontSlot = _envHandler.GetEnvironmentContainer().GetInFrontSlot();
        _towerHealth = _envHandler.GetEnvironmentContainer().GetTower();

        _button.onClick.AddListener(SpellAction);
    }

    private void Update()
    {
        if (IsReady)
            return;

        if (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
            int timer = (int)_cooldownTimer;
            _cooldownText.text = timer.ToString();
        }
        else
        {
            IsReady = true;
            _button.interactable = true;
            float cooldownreduce = _spellCooldown * BonusCooldownReduce;
            _cooldownTimer = _spellCooldown - cooldownreduce;
            _cooldownText.text = string.Empty;
        }
    }

    private void SpellAction()
    {
        if (_inFrontSlot.SetDefendEffect(_spellTime))
        {
            IsReady = false;
            _button.interactable = false;
            _audioSource.Play();
        }
        else
        {
            _towerHealth.SetDefendEffect(_spellTime);

            IsReady = false;
            _button.interactable = false;
            _audioSource.Play();
        }
    }
}
