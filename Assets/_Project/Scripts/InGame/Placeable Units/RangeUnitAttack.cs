
using System.Collections;
using UnityEngine;

public class RangeUnitAttack : UnitAttack
{
    public float DoubleShotChance { get; set; }
    public float SlownessOnShot { get; set; }

    protected string _projectailPrefabPath;
    protected float _projectailSpeed;
    protected float _additionalDamageBonus = 0f;
    protected float _additionalAOEBonus = 0f;

    public override void SetData(UnitConfig config)
    {
        _firerate = config.Firerate;
        _damage = config.Damage;
        _originalFirerate = _firerate;
        _originalDamage = _damage;
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
        _originalFirerate = _firerate;
        _originalDamage = _damage;
        _projectailSpeed = projectailSpeed;
        _projectailPrefabPath = projectailPrefabPath;
    }

    public override void AttackAnimationPoint()
    {
        InAnimation = false;
        if(CurrentTarget != null)
        {
            if(_attackSoundClip != null)
            {
                _audioSource.clip = _attackSoundClip;
                _audioSource.Play();
            }

            if(DoubleShotChance > 0 && OnDoubleShot == false)
            {
                if (DoubleShotChance > Random.Range(0f, 1f))
                    OnDoubleShot = true;
            }
            
            Projectail projectail = Instantiate(Resources.Load<Projectail>(_projectailPrefabPath), transform);
            if (OnEmpoweredShot)
            {
                projectail.Initialize(_damage + _empoweredDamage + _additionalDamageBonus, _projectailSpeed,
                    CurrentTarget.transform, _additionalAOEBonus, SlownessOnShot);
                Vector2 newScale = new(projectail.transform.localScale.x + 0.1f, projectail.transform.localScale.y + 0.1f);
                projectail.transform.localScale = newScale;
                OnEmpoweredShot = false;
                _unitUltimate.UnitPerformedUltimate();
            }
            else
            {
                projectail.Initialize(_damage + _additionalDamageBonus, _projectailSpeed,
                    CurrentTarget.transform, _additionalAOEBonus, SlownessOnShot);
            }

            if(OnDoubleShot)
            {
                StartCoroutine(DoubleShotDelay());
                OnDoubleShot = false;
                _unitUltimate.UnitPerformedUltimate();
            }
            
        }
        
    }

    public void SetAmplification(float additionalDamage, float additionalAOE)
    {
        _additionalDamageBonus = _damage * additionalDamage;
        _additionalAOEBonus = additionalAOE;
    }

    private IEnumerator DoubleShotDelay()
    {
        yield return new WaitForSeconds(0.25f);
        if(CurrentTarget != null)
        {
            Projectail nextprojectail = Instantiate(Resources.Load<Projectail>(_projectailPrefabPath), transform);
            nextprojectail.Initialize((_damage * 0.5f) + _additionalDamageBonus,
                _projectailSpeed, CurrentTarget.transform, _additionalAOEBonus);
        }
        
    }
 
}
