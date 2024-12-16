using UnityEngine;

public class ShowDamageText : MonoBehaviour
{
    public DynamicTextData TextData;

    public void ShowDamage(float dmg)
    {
        DynamicTextManager.CreateText2D(transform.position, dmg.ToString(), TextData);
    }
}
