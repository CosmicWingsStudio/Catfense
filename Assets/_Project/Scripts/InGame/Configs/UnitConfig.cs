using UnityEngine;

[CreateAssetMenu(fileName = "UnitConfig", menuName = "UnitConfig")]
public class UnitConfig : ScriptableObject
{
    [field:SerializeField] public string Name { get; set; }
    [field: SerializeField] public float HealthPoints { get; set; }
    [field: SerializeField] public float Damage { get; set; }
    [field: SerializeField] public string PrefabName { get; set; }
}
