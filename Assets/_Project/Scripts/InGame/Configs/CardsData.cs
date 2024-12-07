using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardsData", menuName = "CardsData")]
public class CardsData : ScriptableObject
{
    [Header("Put the probability for each Card tier")]

    [field: SerializeField, Range(1, 99)] public int T1Weight { get; private set; }
    [field:SerializeField, Range(1, 99)] public int T2Weight { get; private set; }
    [field:SerializeField, Range(1, 99)] public int T3Weight { get; private set; }

    [Header("Populate these lists with prefab names relate to exact Card tier")]

    public List<string> T1Cards = new();
    public List<string> T2Cards = new();
    public List<string> T3Cards = new();

    private void OnValidate()
    {
        if(T1Weight+T2Weight+T3Weight > 100)
        {
            T1Weight = 10;
            T2Weight = 25;
            T3Weight = 65;
        }
    }
}
