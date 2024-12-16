
using UnityEngine;

public class RangeUnitAttack : UnitAttack
{
    protected string _projectailPrefabPath;
    protected float _projectailSpeed;

    public override void SetData(UnitConfig config)
    {
        _firerate = config.Firerate;
        _damage = config.Damage;
        _projectailSpeed = config.ProjectailSpeed;

        if (config.ProjectailPrefabPath == string.Empty == false)
            _projectailPrefabPath = config.ProjectailPrefabPath;
        else
            _projectailPrefabPath = "InGamePrefabs/Projectails/Default";
    }

    public override void SetData(float firerate, int damage, float projectailSpeed, string projectailPrefabPath)
    {
        _firerate = firerate;
        _damage = damage;
        _originalDamage = damage;
        _projectailSpeed = projectailSpeed;
        _projectailPrefabPath = projectailPrefabPath;
    }

    protected override void AttackAnimationPoint()
    {
        Projectail projectail = Instantiate(Resources.Load<Projectail>(_projectailPrefabPath), transform);
        projectail.Initialize(_damage, _projectailSpeed, CurrentTarget.transform);
    }
}
