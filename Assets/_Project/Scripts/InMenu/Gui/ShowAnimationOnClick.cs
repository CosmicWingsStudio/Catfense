using UnityEngine;
using UnityEngine.EventSystems;

public class ShowAnimationOnClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Animator _animator;

    public void OnPointerClick(PointerEventData eventData)
    {
        _animator.SetTrigger("Click");
    }
}
