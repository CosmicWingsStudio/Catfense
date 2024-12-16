using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [field: SerializeField]
    public int Health { get; private set; }

    [field: SerializeField]
    public Slider HealthPointsSlider { get; private set; }
}
