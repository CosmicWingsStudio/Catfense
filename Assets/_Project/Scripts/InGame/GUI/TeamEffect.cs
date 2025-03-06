using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamEffect : MonoBehaviour
{
    private int _numbers;
    public int Numbers
    {
        get { return _numbers; }
        set
        {
            _numbers = value;
            _textNumber.text = _numbers.ToString();
            _effect.UpdateEffect(_numbers);
        }
    }

    [SerializeField] private TextMeshProUGUI _textNumber;
    [SerializeField] private Button _showInfoButton;
    [SerializeField] private TeamEffectInfo _infoObject;

    private TeamEffect_Unit _effect;

    private void Awake()
    {
        _showInfoButton.onClick.AddListener(ShowMoreInfo);
        _effect = GetComponent<TeamEffect_Unit>();
    }

    private void ShowMoreInfo()
    {
        _infoObject.gameObject.SetActive(true);
        Vector2 newPos = transform.position;
        newPos.x += 280f;
        _infoObject.transform.position = newPos;
    }

    //private void InititalizeEffect(TeamEffectsType type)
    //{
    //    switch (type)
    //    {
    //        case TeamEffectsType.Pirate:

    //            break;
    //        case TeamEffectsType.Vampire:
    //            break;
    //        case TeamEffectsType.Snow:
    //            break;
    //        case TeamEffectsType.Alchimist:
    //            break;
    //        case TeamEffectsType.Viking:
    //            break;
    //        case TeamEffectsType.Shooter:
    //            break;
    //        case TeamEffectsType.Magic:
    //            break;
    //        case TeamEffectsType.Aristocratism:
    //            break;
    //        default:
    //            break;
    //    }
    //}

    private void OnDisable()
    {
        Numbers = 0;
        _infoObject.gameObject.SetActive(false);
    }

    public void UpdateEffect(int num)
    {
        Numbers = num;
        _effect.UpdateEffect(Numbers);
    }
}
