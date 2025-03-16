using UnityEngine;

public class ShowDamageText : MonoBehaviour
{
    public DynamicTextData TextData;
    [SerializeField] private float _offsetY;

    public void ShowDamage(int dmg)
    {
        Vector2 newVec = new(transform.position.x, transform.position.y + _offsetY);
        DynamicTextManager.CreateText2D(newVec, dmg.ToString(), TextData);
    }
}
