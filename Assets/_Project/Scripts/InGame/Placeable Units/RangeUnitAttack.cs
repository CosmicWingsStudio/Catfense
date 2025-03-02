
using System.Collections;
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
            
            Projectail projectail = Instantiate(Resources.Load<Projectail>(_projectailPrefabPath), transform);
            if (OnEmpoweredShot)
            {
                projectail.Initialize(_damage + _empoweredDamage, _projectailSpeed, CurrentTarget.transform);
                Vector2 newScale = new(projectail.transform.localScale.x + 0.1f, projectail.transform.localScale.y + 0.1f);
                projectail.transform.localScale = newScale;
                OnEmpoweredShot = false;
                _unitUltimate.UnitPerformedUltimate();
            }
            else
            {
                projectail.Initialize(_damage, _projectailSpeed, CurrentTarget.transform);
            }

            if(OnDoubleShot)
            {
                StartCoroutine(DoubleShotDelay());
                OnDoubleShot = false;
                _unitUltimate.UnitPerformedUltimate();
            }
            
        }
        
    }

    private IEnumerator DoubleShotDelay()
    {
        yield return new WaitForSeconds(0.25f);
        Projectail nextprojectail = Instantiate(Resources.Load<Projectail>(_projectailPrefabPath), transform);
        nextprojectail.Initialize(_damage * 0.5f, _projectailSpeed, CurrentTarget.transform);
    }
 
}
