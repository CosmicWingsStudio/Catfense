

using System.Collections;
using UnityEngine;

public class AdditionalTowerPart : UnityEngine.MonoBehaviour
{
    [field:UnityEngine.SerializeField]
    public UnityEngine.Transform BuyableSlotsFolder { get; private set; }

    private void OnEnable()
    {
        GameObject[] childrens = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            childrens[i] = transform.GetChild(i).gameObject;
        }

        StartCoroutine(VisibilityDelay(childrens));
    }
    private IEnumerator VisibilityDelay(GameObject[] childrens)
    {
        for (int i = 0; i < childrens.Length; i++)
        {
            childrens[i].SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < childrens.Length; i++)
        {
            childrens[i].SetActive(true);
        }
    }
}
