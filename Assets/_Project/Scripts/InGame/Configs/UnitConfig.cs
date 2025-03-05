using UnityEngine;

[CreateAssetMenu(fileName = "UnitConfig", menuName = "UnitConfig")]
public class UnitConfig : ScriptableObject
{
    [field:SerializeField, Tooltip("must be unique")] public string Name { get; set; }
    [field: SerializeField] public string PresentiveName { get; set; }
    [field: SerializeField] public int HealthPoints { get; set; }
    [field: SerializeField] public int Damage { get; set; }
    [field: SerializeField] public float Firerate { get; set; }
    [field: SerializeField] public int Price { get; set; }
    [field: SerializeField] public TeamEffectsType TeamEffectCollection { get; set; }
    [field: SerializeField] public string PrefabName { get; set; }
    [field: SerializeField] public Sprite UnitSprite { get; set; }
    [field: SerializeField, Tooltip("Range or Melee")] public string UnitAttackType { get; set; }

    [field:Header("if RANGE unit")]
    [field: SerializeField] public string ProjectailPrefabPath { get; set; }
    [field: SerializeField] public float ProjectailSpeed { get; set; }
}
