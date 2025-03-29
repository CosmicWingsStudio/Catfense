using UnityEngine;
using UnityEngine.EventSystems;

public class ShowAnimationOnClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip _sound;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _sound;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _animator.SetTrigger("Click");
        _audioSource.Play();
    }
}
